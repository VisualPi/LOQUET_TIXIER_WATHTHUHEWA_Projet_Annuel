using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameColorManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _cases;

    [SerializeField]
    private List<ColorCase> _colorCases;

    void Start()
    {
        Utils.Instance.playerBlue.GetAnimator().SetTrigger("Fall");
        Utils.Instance.playerGreen.GetAnimator().SetTrigger("Fall");
        Utils.Instance.playerRed.GetAnimator().SetTrigger("Fall");
        Utils.Instance.playerYellow.GetAnimator().SetTrigger("Fall");
    }
    [SerializeField]
    private float _gameDuration = 60;
    private float _timePerStep = 0.5f;


    private float _timeLapEllapsed = 0f;//temps avant gain de point
    private float _timeGameEllapsed = 0f;//temps du jeu
    private float _timePerStepEllapsed = 0f;//temps par mouvement

    [SerializeField]
    private bool _gameStarted;

    public bool GetCaseByPos(Vector3 position)
    {
        foreach (var c in _cases)
        {
            if (c.position.x == (int)position.x && c.position.z == (int)position.z)
                return true;
        }
        return false;
    }

    private bool ComparePos(Vector3 pos1, Vector3 pos2)
    {
        return Mathf.Abs(pos1.x - pos2.x) <= 0.1f && Mathf.Abs(pos1.z - pos2.z) <= 0.1f;
    }

    private bool HasPlayerOnCase(EPlayer player, Vector3 pos)
    {
        switch (player)
        {
            case EPlayer.BLUE:
                return (
                        ComparePos(Utils.Instance.playerGreen.transform.position, pos)
                    || ComparePos(Utils.Instance.playerRed.transform.position, pos)
                    || ComparePos(Utils.Instance.playerYellow.transform.position, pos)
                    );
            case EPlayer.GREEN:
                return (
                       ComparePos(Utils.Instance.playerBlue.transform.position, pos)
                    || ComparePos(Utils.Instance.playerRed.transform.position, pos)
                    || ComparePos(Utils.Instance.playerYellow.transform.position, pos)
                    );
            case EPlayer.RED:
                return (
                        ComparePos(Utils.Instance.playerGreen.transform.position, pos)
                    || ComparePos(Utils.Instance.playerBlue.transform.position, pos)
                    || ComparePos(Utils.Instance.playerYellow.transform.position, pos)
                    );
            case EPlayer.YELLOW:
                return (
                       ComparePos(Utils.Instance.playerGreen.transform.position, pos)
                    || ComparePos(Utils.Instance.playerRed.transform.position, pos)
                    || ComparePos(Utils.Instance.playerBlue.transform.position, pos)
                    );
            default:
                return false;
        }
    }

    private void MovePlayers()
    {
        for (var i = 0; i < 4; i++)
        {
            var pos = Utils.Instance.GetPlayerByColor((EPlayer)i).GetComponent<PlayerMovement>().nextStep;
            var c = GetCaseByPos(new Vector3(pos.x, 0f, pos.z));
            if (c && !HasPlayerOnCase((EPlayer)i, pos))
                Utils.Instance.GetPlayerByColor((EPlayer)i).transform.position = pos;
        }
    }

    [SerializeField]
    private int _bluePoints;
    [SerializeField]
    private int _redPoints;
    [SerializeField]
    private int _greenPoints;
    [SerializeField]
    private int _yellowPoints;
    private void GetPoints()
    {
        foreach (var c in _colorCases)
        {
            if (c.GetColor() == EColorCaseType.BLUE)
                _bluePoints++;
            if (c.GetColor() == EColorCaseType.GREEN)
                _greenPoints++;
            if (c.GetColor() == EColorCaseType.RED)
                _redPoints++;
            if (c.GetColor() == EColorCaseType.YELLOW)
                _yellowPoints++;
        }
        //afficher les points sur le HUD
    }

    void Update()
    {
        if (_gameStarted)
        {
            _timeGameEllapsed += Time.deltaTime;
            _timeLapEllapsed += Time.deltaTime;
            _timePerStepEllapsed += Time.deltaTime;
            if (_timeGameEllapsed >= _gameDuration)
            {
                Debug.Log("Game Finished !!");
                _gameStarted = false;
                _timeGameEllapsed = 0f;
                //playerPref le winner
            }
            if (_timePerStepEllapsed >= _timePerStep)
            {
                MovePlayers();
                _timePerStepEllapsed = 0f;
            }
            if (_timeLapEllapsed >= 10f)
            {
                GetPoints();
                _timeLapEllapsed = 0f;
            }
        }
    }
}
