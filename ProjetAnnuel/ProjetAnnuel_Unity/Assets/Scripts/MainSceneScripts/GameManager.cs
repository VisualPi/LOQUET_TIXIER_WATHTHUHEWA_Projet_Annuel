using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<EPlayer> _playerOrder;

    private EPlayer _currentPlayer;

    [SerializeField]
    private CameraManager _cameraManager;

    [SerializeField]
    private Cinematique _cinematique;

    void Start()
    {
        //TMP

       //PlayerPrefs.SetString("PLAYER_BLUE_NAME", "NONAME1");
       //PlayerPrefs.SetString("PLAYER_GREEN_NAME", "NONAME2");
       //PlayerPrefs.SetString("PLAYER_RED_NAME", "NONAME3");
       //PlayerPrefs.SetString("PLAYER_YELLOW_NAME", "NONAME4");
	   //
       //PlayerPrefs.SetInt("PLAYER_BLUE_ISAI", 0);
       //PlayerPrefs.SetInt("PLAYER_GREEN_ISAI", 0);
       //PlayerPrefs.SetInt("PLAYER_RED_ISAI", 0);
       //PlayerPrefs.SetInt("PLAYER_YELLOW_ISAI", 0);
	   //
       //PlayerPrefs.SetInt("PLAYER_BLUE_STARS", 0);
       //PlayerPrefs.SetInt("PLAYER_GREEN_STARS", 0);
       //PlayerPrefs.SetInt("PLAYER_RED_STARS", 0);
       //PlayerPrefs.SetInt("PLAYER_YELLOW_STARS", 0);
	   //
       //PlayerPrefs.SetInt("PLAYER_BLUE_CASEID", 0);
       //PlayerPrefs.SetInt("PLAYER_GREEN_CASEID", 0);
       //PlayerPrefs.SetInt("PLAYER_RED_CASEID", 0);
       //PlayerPrefs.SetInt("PLAYER_YELLOW_CASEID", 0);
	   //
        PlayerPrefs.SetInt("GAME_ISFIRSTRUN", 1);
        
        //TMP







        _playerOrder = new List<EPlayer>(4);
        Utils.Instance.GetPlayerByColor(EPlayer.BLUE).SetName(PlayerPrefs.GetString("PLAYER_BLUE_NAME"));
        Utils.Instance.GetPlayerByColor(EPlayer.RED).SetName(PlayerPrefs.GetString("PLAYER_GREEN_NAME"));
        Utils.Instance.GetPlayerByColor(EPlayer.GREEN).SetName(PlayerPrefs.GetString("PLAYER_RED_NAME"));
        Utils.Instance.GetPlayerByColor(EPlayer.YELLOW).SetName(PlayerPrefs.GetString("PLAYER_YELLOW_NAME"));
        Utils.Instance.GetPlayerByColor(EPlayer.BLUE).SetIsAI(PlayerPrefs.GetInt("PLAYER_BLUE_ISAI") == 1 ? true : false);
        Utils.Instance.GetPlayerByColor(EPlayer.GREEN).SetIsAI(PlayerPrefs.GetInt("PLAYER_GREEN_ISAI") == 1 ? true : false);
        Utils.Instance.GetPlayerByColor(EPlayer.RED).SetIsAI(PlayerPrefs.GetInt("PLAYER_RED_ISAI") == 1 ? true : false);
        Utils.Instance.GetPlayerByColor(EPlayer.YELLOW).SetIsAI(PlayerPrefs.GetInt("PLAYER_YELLOW_ISAI") == 1 ? true : false);


        if (PlayerPrefs.GetInt("GAME_ISFIRSTRUN") == 1) //si jamais c'est le premier lancement de la scene on met GAME_ISFIRSTRUN a 0
        {
            PlayerPrefs.SetInt("GAME_ISFIRSTRUN", 0);
            //on place les joueurs sur la case départ
            Utils.Instance.GetPlayerByColor(EPlayer.BLUE).transform.position = Utils.Instance.GetDepartCase().GetCasePosition(EPlayer.BLUE, false);
            Utils.Instance.GetPlayerByColor(EPlayer.GREEN).transform.position = Utils.Instance.GetDepartCase().GetCasePosition(EPlayer.GREEN, false);
            Utils.Instance.GetPlayerByColor(EPlayer.RED).transform.position = Utils.Instance.GetDepartCase().GetCasePosition(EPlayer.RED, false);
            Utils.Instance.GetPlayerByColor(EPlayer.YELLOW).transform.position = Utils.Instance.GetDepartCase().GetCasePosition(EPlayer.YELLOW, false);
            PlayerPrefs.SetInt("PLAYER_BLUE_CASEID", 0);
            PlayerPrefs.SetInt("PLAYER_GREEN_CASEID", 0);
            PlayerPrefs.SetInt("PLAYER_RED_CASEID", 0);
            PlayerPrefs.SetInt("PLAYER_YELLOW_CASEID", 0);
        }
        else if (PlayerPrefs.GetInt("GAME_ISFIRSTRUN") == 0)//si ce n'est pas le premier lancement de la scene on lance pas la cinématique
        {
            _cinematique.SetCinematiqueFinished(true);
            _cinematique.gameObject.SetActive(false);
            var id = PlayerPrefs.GetInt("PLAYER_BLUE_CASEID");
            Utils.Instance.GetPlayerByColor(EPlayer.BLUE).transform.position = Utils.Instance.GetCaseByID(id).GetCasePosition(EPlayer.BLUE, true);
            Utils.Instance.GetPlayerByColor(EPlayer.BLUE).SetCaseID(id);
            id = PlayerPrefs.GetInt("PLAYER_GREEN_CASEID");
            Utils.Instance.GetPlayerByColor(EPlayer.GREEN).transform.position = Utils.Instance.GetCaseByID(id).GetCasePosition(EPlayer.GREEN, true);
            Utils.Instance.GetPlayerByColor(EPlayer.GREEN).SetCaseID(id);
            id = PlayerPrefs.GetInt("PLAYER_RED_CASEID");
            Utils.Instance.GetPlayerByColor(EPlayer.RED).transform.position = Utils.Instance.GetCaseByID(id).GetCasePosition(EPlayer.RED, true);
            Utils.Instance.GetPlayerByColor(EPlayer.RED).SetCaseID(id);
            id = PlayerPrefs.GetInt("PLAYER_YELLOW_CASEID");
            Utils.Instance.GetPlayerByColor(EPlayer.YELLOW).transform.position = Utils.Instance.GetCaseByID(id).GetCasePosition(EPlayer.YELLOW, true);
            Utils.Instance.GetPlayerByColor(EPlayer.YELLOW).SetCaseID(id);
        }

        _currentPlayer = EPlayer.BLUE;//TODO:choisir par l'ordre aléatoire (lancé de dé)

    }
    private int diceNumber = -1;//nombre de déplacement choisi par le dé
    private int currentNumber = 1;//nombre actuel de déplacement
    bool diceChoosed = false;

    [SerializeField]
    private bool BLA = false;

    // Update is called once per frame
    void Update()
    {
        if (BLA)
            SceneManager.LoadScene("main");
        if (_cinematique.GetCinematiqueFinished())
        {
            _cinematique.gameObject.SetActive(false);
            _cameraManager.GetMainCamera().gameObject.SetActive(true);
            GetComponent<Animator>().SetBool("IsCinematiqueFinished", true);
        }
    }


    public EPlayer GetNextPlayer()//TODO: faire un ordre aleatoire en début de partie
    {
        switch (_currentPlayer)
        {
            case EPlayer.BLUE:
                return EPlayer.GREEN;
            case EPlayer.GREEN:
                return EPlayer.RED;
            case EPlayer.RED:
                return EPlayer.YELLOW;
            case EPlayer.YELLOW:
                return EPlayer.BLUE;
            default:
                return EPlayer.BLUE;
        }
    }

    public EPlayer GetCurrentPlayer()
    {
        return _currentPlayer;
    }
    public void NextPlayer()
    {
        _currentPlayer = GetNextPlayer();
    }
    public void SetDiceNumber(int number)
    {
        diceNumber = number;
    }
    public int GetDiceNumber()
    {
        return diceNumber;
    }
}
