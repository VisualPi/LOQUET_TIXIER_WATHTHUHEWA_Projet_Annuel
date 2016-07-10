using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum EState { CAMERAZOOM, WHEELSPIN, PLAYERPOINTING, WHEELFREEZE, CAMERARESET, PLAYERMOVE, CALLMINIGAME }

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
	private float _startTime;
	private Vector3 _startMarker;
	private Vector3 _endMarker;
	private bool _arriveAtCase = true;

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
		_speed = 50;
		_enterPlayerMoveState = true;
		_arriveAtCase = true;
		t = 0f;
	}


	void Update()
	{
		if( _cinematique.GetCinematiqueFinished() )
		{
			switch( _state )
			{
			case EState.CAMERAZOOM:
				{
					if( _enteringCameraState )
					{
						GetComponent<CameraManager>().GetMainCamera().SetActive(false);
						GetComponent<CameraManager>().GetMainCamera().GetComponent<AudioListener>().enabled = false;
						GetComponent<CameraManager>().GetPlayerCamera().SetActive(true);
						GetComponent<CameraManager>().GetRoulette().gameObject.SetActive(true);
						GetComponent<CameraManager>().GetRoulette().gameObject.SetActive(false);
						GetComponent<CameraManager>().GetPlayerCamera().GetComponent<AudioListener>().enabled = true;
						GetComponent<CameraManager>().GetPlayerCanvas().SetActive(true);
						_enteringCameraState = false;
					}
					var pos = Utils.Instance.GetPlayerByColor(GetComponent<GameManager>().GetCurrentPlayer()).transform.position;
					GetComponent<CameraManager>().GetPlayerCamera().transform.position = Vector3.Lerp(GetComponent<CameraManager>().GetPlayerCamera().transform.position,
																												new Vector3(pos.x + 4, pos.y + 5, pos.z - 15),
																												t);

					var tr = Utils.Instance.GetPlayerByColor(GetComponent<GameManager>().GetCurrentPlayer()).transform;
					GetComponent<CameraManager>().GetPlayerCamera().transform.LookAt(tr.position + (tr.up *4)	);
					GetComponent<CameraManager>().GetRoulette().transform.position = tr.position + (-tr.right *8) + (tr.up * 4);
                    var trC = GetComponent<CameraManager>().GetPlayerCamera().transform;

					if( t < 1 )
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
					if( _enteringWheelSpinState )
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
					if( !_isPointingStarted )
					{
						if( !_isButtonHitted )
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
						if( Utils.Instance.GetPlayerByColor(GetComponent<GameManager>().GetCurrentPlayer()).GetAnimPointMiddle() )
						{
							_state++;
						}
					}
					break;
				}
			case EState.WHEELFREEZE:
				{
					if( _enterWheelStopState )
					{
						var number = Random.Range(1, 9);
						GetComponent<GameManager>().SetDiceNumber(number);
						GetComponent<CameraManager>().GetRoulette().StopSpin(number);
						_enterWheelStopState = false;
					}
					if( _timeEllapsedWheelStop >= _timeAfterWheelStop )
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
					if( _enterPlayerMoveState )
					{
						_iteration = 0;
						_currentPlayer = Utils.Instance.GetPlayerByColor(GetComponent<GameManager>().GetCurrentPlayer());
						_currentPlayer.ResetAnim();
						_currentPlayer.GetAnimator().Play("running_inPlace");
						_enterPlayerMoveState = false;
					}
					else
					{
						if( _arriveAtCase )
						{
							_iteration++;
							var nextID = Utils.Instance.GetCaseByID(_currentPlayer.GetCaseID()).GetNextCaseID();
							_currentPlayer.SetCaseID(nextID);
							if( Utils.Instance.GetCaseByID(nextID).GetCaseType() == ECaseType.INTERSECTION )
								GetComponent<GameManager>().SetDiceNumber(GetComponent<GameManager>().GetDiceNumber() + 1);


							switch( _currentPlayer.GetPlayerColor() )//a voir ! semble suffir player.SetCaseID(id)
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

							_startMarker = _currentPlayer.transform.position;
							_endMarker = Utils.Instance.GetCaseByID(nextID).GetCasePosition(_currentPlayer.GetPlayerColor(), false);
							_startTime = Time.time;
							_arriveAtCase = false;
						}
						else
						{
							float animLength = Vector3.Distance(_startMarker, _endMarker);
							float distCov = (Time.time - _startTime) * _speed;
							float fracJourney = distCov / animLength;
							_currentPlayer.transform.position = Vector3.Lerp(_startMarker, _endMarker, fracJourney);
							_currentPlayer.transform.LookAt(_endMarker);
							if( ( Mathf.Abs(_currentPlayer.transform.position.x - _endMarker.x) <= 0.01f )
								&& ( Mathf.Abs(_currentPlayer.transform.position.z - _endMarker.z) <= 0.01f ) )
							{
								_arriveAtCase = true;
								if( _iteration == GetComponent<GameManager>().GetDiceNumber() )
								{
									Debug.Log("iter = " + _iteration);
									_currentPlayer.GetAnimator().Play("idle");
									GetComponent<CameraManager>().UnfocusOnPlayer(_currentPlayer.GetPlayerColor());
									GetComponent<GameManager>().NextPlayer();
									if( _currentPlayer.GetPlayerColor() != EPlayer.YELLOW )
									{
										_state = EState.CAMERAZOOM;
									}
									else
									{
										_state = EState.CALLMINIGAME;
									}
									InitValeurs();
								}
							}
						}

					}
					break;
				}
			case EState.CALLMINIGAME:
				{
                        int random;
                        random = (Random.Range(0, 10) % 2);
                        Debug.Log(random);
                        if(random!=0)
                        {
                            SceneManager.LoadScene(3);
                        }
                        else
                        {
                            SceneManager.LoadScene(4);
                        }

                        break;
				}
			}

		}
	}

	private void GetGoodInput( EPlayer player )
	{
		switch( player )
		{
		case EPlayer.BLUE:
			if( PlayerPrefs.GetInt("PLAYER_BLUE_ISAI") == 1 )
				_isButtonHitted = true;
			if( Input.GetKey(KeyCode.Joystick1Button0) )
				_isButtonHitted = true;
			break;
		case EPlayer.GREEN:
			if( PlayerPrefs.GetInt("PLAYER_GREEN_ISAI") == 1 )
				_isButtonHitted = true;
			if( Input.GetKeyDown(KeyCode.Joystick2Button0) )
				_isButtonHitted = true;
			break;
		case EPlayer.RED:
			if( PlayerPrefs.GetInt("PLAYER_RED_ISAI") == 1 )
				_isButtonHitted = true;
			if( Input.GetKeyDown(KeyCode.Joystick3Button0) )
				_isButtonHitted = true;
			break;
		case EPlayer.YELLOW:
			if( PlayerPrefs.GetInt("PLAYER_YELLOW_ISAI") == 1 )
				_isButtonHitted = true;
			if( Input.GetKeyDown(KeyCode.Joystick4Button0) )
				_isButtonHitted = true;
			break;
		}

	}
}