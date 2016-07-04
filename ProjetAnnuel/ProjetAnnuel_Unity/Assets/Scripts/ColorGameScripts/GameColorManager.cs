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

    private Vector2[][] _logicalCases;

    void Start()
    {
        _logicalCases = new Vector2[10][];
        for (var i = 0; i < 10; i++)
            _logicalCases[i] = new Vector2[10];
        for (var i = 0; i < 10; i++)
            for (var j = 0; j < 10; j++)
                _logicalCases[i][j] = new Vector2(i * 5, -j * 5);

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

    public bool IsCaseByPos(Vector3 position)
    {
        foreach (var c in _colorCases)
        {
            if (c.transform.position.x == (int)position.x && c.transform.position.z == (int)position.z)
                return true;
        }
        return false;
    }

    public ColorCase GetCaseByPos(Vector3 position)
    {
        foreach (var c in _colorCases)
        {
            if (c.transform.position.x == (int)position.x && c.transform.position.z == (int)position.z)
                return c;
        }
        return null;
    }
    public ColorCase GetCaseByPos(Vector2 position)
    {
        foreach (var c in _colorCases)
        {
            if (c.transform.position.x == (int)position.x && c.transform.position.z == (int)position.y)
                return c;
        }
        return null;
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
            var c = IsCaseByPos(new Vector3(pos.x, 0f, pos.z));
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

    private bool GetNeighboors(ColorCase c, EColorCaseType colorSearched)
    {
        var nbNeighboors = 0;
        var cn = GetCaseByPos(new Vector2(c.transform.position.x + 5f, c.transform.position.z));
        if (cn && cn.GetColor() == colorSearched)
            nbNeighboors++;
        cn = GetCaseByPos(new Vector2(c.transform.position.x - 5f, c.transform.position.z));
        if (cn && cn.GetColor() == colorSearched)
            nbNeighboors++;
        cn = GetCaseByPos(new Vector2(c.transform.position.x, c.transform.position.z + 5f));
        if (cn && cn.GetColor() == colorSearched)
            nbNeighboors++;
        cn = GetCaseByPos(new Vector2(c.transform.position.x, c.transform.position.z - 5f));
        if (cn && cn.GetColor() == colorSearched)
            nbNeighboors++;

        return nbNeighboors >= 2;
    }

    private void CheckForSquares()
    {
        List<Vector2> tmpFill = new List<Vector2>();
        bool possibleBegin = false;
        for (var i = 1; i < 5; i++)
        {
            for (var y = 1; y < 9; y++)//skip la premiere et derniere ligne
            {
                for (var x = 1; x < 9; x++)//skip la premiere et derniere colonne
                {
                    if (GetCaseByPos(_logicalCases[x][y]).GetColor() == (EColorCaseType)i)
                    {

                    }
                }
                
            }

        }

    }


    private void Remplissage(ColorCase c, EColorCaseType colorcible, EColorCaseType colorRepere)
    {
        if (c.GetColor() != colorRepere)//si la couleur est autre que la couleur contour
        {
            c.SetColor(colorRepere);
            var pos = new Vector3(c.transform.position.x, c.transform.position.y, c.transform.position.z + 5f); //case Nord
            if (GetCaseByPos(pos))
                Remplissage(GetCaseByPos(pos), colorcible, colorRepere);

            pos = new Vector3(c.transform.position.x, c.transform.position.y, c.transform.position.z - 5f); //case Sud
            if (GetCaseByPos(pos))
                Remplissage(GetCaseByPos(pos), colorcible, colorRepere);

            pos = new Vector3(c.transform.position.x + 5f, c.transform.position.y, c.transform.position.z); //case Est
            if (GetCaseByPos(pos))
                Remplissage(GetCaseByPos(pos), colorcible, colorRepere);

            pos = new Vector3(c.transform.position.x - 5f, c.transform.position.y, c.transform.position.z - 5f); //case ouest
            if (GetCaseByPos(pos))
                Remplissage(GetCaseByPos(pos), colorcible, colorRepere);
        }
    }


    [SerializeField]
    private bool checkForSquare;

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
