using UnityEngine;
using System.Collections;

public enum EState { CAMERAZOOM, WHEELSPIN, PLAYERPOINTING, WHEELFREEZE, CAMERARESET, PLAYERMOVE }

public class ScriptStateMachine : MonoBehaviour
{
    [SerializeField]
    private Cinematique _cinematique;

    [SerializeField]
    private EState _state;

    private float _timeCameraZoom = 2f;
    private bool _enteringCameraState = true;

    private bool _enteringWheelSpinState = true;

    private bool _isButtonHitted = false;
    private bool _isPointingStarted = false;

    private float _timeAfterWheelStop = 1f;
    private float _timeEllapsedWheelStop = 0f;
    private bool _enterWheelStopState = true;

    [SerializeField]
    private float _speed = 20;
    private bool _enterPlayerMoveState = true;
    private Player _currentPlayer;
    private int _iteration;
    private float _timePerStep;
    private Vector3 _startMarker;
    private Vector3 _endMarker;

    private float t = 0f;

    private void InitValeurs()
    {
        _timeCameraZoom = 2f;
        _enteringCameraState = true;
        _enteringWheelSpinState = true;
        _isButtonHitted = false;
        _isPointingStarted = false;
        _timeAfterWheelStop = 1f;
        _timeEllapsedWheelStop = 0f;
        _enterWheelStopState = true;
        _enterPlayerMoveState = true;
        t = 0f;

    }


    void Update()
    {
        if (_cinematique.GetCinematiqueFinished())
        {
            switch (_state)
            {
                case EState.CAMERAZOOM:
                    {
                        if (_enteringCameraState)
                        {
                            GetComponent<CameraManager>().GetMainCamera().SetActive(false);
                            GetComponent<CameraManager>().GetMainCamera().GetComponent<AudioListener>().enabled = false;
                            GetComponent<CameraManager>().GetPlayerCamera().SetActive(true);
                            GetComponent<CameraManager>().GetRoulette().gameObject.SetActive(false);
                            GetComponent<CameraManager>().GetPlayerCamera().GetComponent<AudioListener>().enabled = true;
                            GetComponent<CameraManager>().GetPlayerCanvas().SetActive(true);
                            _enteringCameraState = false;
                        }
                        var pos = Utils.Instance.GetPlayerByColor(GetComponent<GameManager>().GetCurrentPlayer()).transform.position;
                        GetComponent<CameraManager>().GetPlayerCamera().transform.position = Vector3.Lerp(GetComponent<CameraManager>().GetMainCamera().transform.position,
                                                                                                                    new Vector3(pos.x + 4, pos.y + 5, pos.z - 15),
                                                                                                                    t);
                        if (t < 1)
                            t += Time.deltaTime / _timeCameraZoom;
                        else
                        {
                            _state++;
                            Debug.Log("camera zoom finish ! state is now : " + _state);
                        }
                        break;
                    }
                case EState.WHEELSPIN:
                    {
                        if (_enteringWheelSpinState)
                        {
                            GetComponent<CameraManager>().GetRoulette().gameObject.SetActive(true);
                            GetComponent<CameraManager>().GetNameText().SetText(Utils.Instance.GetPlayerByColor(GetComponent<GameManager>().GetCurrentPlayer()).GetName().ToUpper());
                            GetComponent<CameraManager>().GetNameText().PlayAnim();
                            GetComponent<CameraManager>().GetTurnText().PlayAnim();
                            GetComponent<CameraManager>().GetRoulette().StartSpin();
                            _enteringWheelSpinState = false;
                            _state++;
                        }
                        break;
                    }
                case EState.PLAYERPOINTING:
                    {
                        if (!_isPointingStarted)
                        {
                            if (!_isButtonHitted)
                            {
                                GetGoodInput(GetComponent<GameManager>().GetCurrentPlayer());
                            }
                            else
                            {
                                Utils.Instance.GetPlayerByColor(GetComponent<GameManager>().GetCurrentPlayer()).PlayAnimPointing();
                                _isPointingStarted = true;
                            }
                        }
                        else
                        {
                            if (Utils.Instance.GetPlayerByColor(GetComponent<GameManager>().GetCurrentPlayer()).GetAnimPointMiddle())
                            {
                                _state++;
                            }
                        }
                        break;
                    }
                case EState.WHEELFREEZE:
                    {
                        if (_enterWheelStopState)
                        {
                            var number = Random.Range(1, 9);
                            GetComponent<GameManager>().SetDiceNumber(number);
                            GetComponent<CameraManager>().GetRoulette().StopSpin(number);
                            _enterWheelStopState = false;
                        }
                        if (_timeEllapsedWheelStop >= _timeAfterWheelStop)
                        {
                            _state++;
                            _timeEllapsedWheelStop = 0f;
                        }
                        else
                            _timeEllapsedWheelStop += Time.deltaTime;
                        break;
                    }
                case EState.CAMERARESET:
                    {
                        GetComponent<CameraManager>().UnfocusOnPlayer(GetComponent<GameManager>().GetCurrentPlayer());
                        GetComponent<CameraManager>().GetRoulette().gameObject.SetActive(false);
                        _state++;
                        break;
                    }
                case EState.PLAYERMOVE:
                    {
                        if (_enterPlayerMoveState)
                        {
                            _iteration = 0;
                            _currentPlayer = Utils.Instance.GetPlayerByColor(GetComponent<GameManager>().GetCurrentPlayer());
                            _currentPlayer.ResetAnim();
                            _currentPlayer.GetAnimator().Play("running_inPlace");
                            _enterPlayerMoveState = false;
                        }
                        else
                        {
                            if (_timePerStep >= 1f)//1 seconde par case
                            {
                                int currentIteration = _iteration++;
                                if (currentIteration == GetComponent<GameManager>().GetDiceNumber())
                                {
                                    Debug.Log("iter = " + currentIteration);
                                    _currentPlayer.GetAnimator().Play("idle");
                                    GetComponent<CameraManager>().UnfocusOnPlayer(_currentPlayer.GetPlayerColor());
                                    GetComponent<GameManager>().NextPlayer();
                                    _state = EState.CAMERAZOOM;
                                    return;
                                }
                                else
                                {
                                    var nextID = Utils.Instance.GetCaseByID(_currentPlayer.GetCaseID()).GetNextCaseID();
                                    _currentPlayer.SetCaseID(nextID);
                                    if (Utils.Instance.GetCaseByID(nextID).GetCaseType() == ECaseType.INTERSECTION)
                                        GetComponent<GameManager>().SetDiceNumber(GetComponent<GameManager>().GetDiceNumber() + 1);

                                    _startMarker = _currentPlayer.transform.position;
                                    _endMarker = Utils.Instance.GetCaseByID(nextID).GetCasePosition(_currentPlayer.GetPlayerColor(), false);
                                    float animLength = Vector3.Distance(_startMarker, _endMarker);

                                    switch (_currentPlayer.GetPlayerColor())//a voir ! semble suffir player.SetCaseID(id)
                                    {
                                        case EPlayer.BLUE:
                                            PlayerPrefs.SetInt("PLAYER_BLUE_CASEID", nextID);
                                            break;
                                        case EPlayer.GREEN:
                                            PlayerPrefs.SetInt("PLAYER_GREEN_CASEID", nextID);
                                            break;
                                        case EPlayer.RED:
                                            PlayerPrefs.SetInt("PLAYER_RED_CASEID", nextID);
                                            break;
                                        case EPlayer.YELLOW:
                                            PlayerPrefs.SetInt("PLAYER_YELLOW_CASEID", nextID);
                                            break;
                                    }


                                    //animator.SetFloat("MoveInverseDuration", _speed / animLength);
                                    float fracJourney = Time.deltaTime / animLength;
                                    _currentPlayer.transform.position = Vector3.Lerp(_startMarker, _endMarker, fracJourney);
                                    _currentPlayer.transform.LookAt(_endMarker);

                                    _timePerStep = 0f;
                                }
                            }
                            else
                                _timePerStep += Time.deltaTime;

                        }
                        break;
                    }
            }

        }
    }

    private void GetGoodInput(EPlayer player)
    {
        switch (player)
        {
            case EPlayer.BLUE:
                if (Input.GetKey(KeyCode.Joystick1Button0))
                    _isButtonHitted = true;
                break;
            case EPlayer.GREEN:
                if (Input.GetKeyDown(KeyCode.Joystick2Button0))
                    _isButtonHitted = true;
                break;
            case EPlayer.RED:
                if (Input.GetKeyDown(KeyCode.Joystick3Button0))
                    _isButtonHitted = true;
                break;
            case EPlayer.YELLOW:
                if (Input.GetKeyDown(KeyCode.Joystick4Button0))
                    _isButtonHitted = true;
                break;
        }

    }
}