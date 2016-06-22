using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	private List<int> playersCaseID;//list contenant l'ID de la case de chaque joueur dans l'ordre de l'enum EPlayer. Par exemple playersCaseID[0] correspondra a la case sur laquelle est le joueur 0 (bleu)

	private EPlayer _currentPlayer;

	[SerializeField]
	private CameraManager _cameraManager;

	[SerializeField]
	private Cinematique _cinematique;

	void Start()
	{
		playersCaseID = new List<int>(4);
		for( int i = 0 ; i < 4 ; i++ )
			playersCaseID.Insert(i, 0);
		Utils.Instance.GetPlayerByColor(EPlayer.BLUE).SetName(PlayerPrefs.GetString("playerBlueName"));
		Utils.Instance.GetPlayerByColor(EPlayer.RED).SetName(PlayerPrefs.GetString("playerRedName"));
		Utils.Instance.GetPlayerByColor(EPlayer.GREEN).SetName(PlayerPrefs.GetString("playerGreenName"));
		Utils.Instance.GetPlayerByColor(EPlayer.YELLOW).SetName(PlayerPrefs.GetString("playerYellowName"));
		Utils.Instance.GetPlayerByColor(EPlayer.BLUE).SetIsAI(PlayerPrefs.GetInt("playerBlueIsAI") == 1 ? true : false);
		Utils.Instance.GetPlayerByColor(EPlayer.BLUE).SetIsAI(PlayerPrefs.GetInt("playerRedIsAI") == 1 ? true : false);
		Utils.Instance.GetPlayerByColor(EPlayer.BLUE).SetIsAI(PlayerPrefs.GetInt("playerGreenIsAI") == 1 ? true : false);
		Utils.Instance.GetPlayerByColor(EPlayer.BLUE).SetIsAI(PlayerPrefs.GetInt("playerYellowIsAI") == 1 ? true : false);

		Utils.Instance.GetPlayerByColor(EPlayer.BLUE).transform.position = Utils.Instance.GetDepartCase().GetCasePosition(EPlayer.BLUE);
		Utils.Instance.GetPlayerByColor(EPlayer.GREEN).transform.position = Utils.Instance.GetDepartCase().GetCasePosition(EPlayer.GREEN);
		Utils.Instance.GetPlayerByColor(EPlayer.RED).transform.position = Utils.Instance.GetDepartCase().GetCasePosition(EPlayer.RED);
		Utils.Instance.GetPlayerByColor(EPlayer.YELLOW).transform.position = Utils.Instance.GetDepartCase().GetCasePosition(EPlayer.YELLOW);
		_currentPlayer = EPlayer.BLUE;//TODO:choisir par l'ordre aléatoire (lancé de dé)
	}
	private int diceNumber = -1;//nombre de déplacement choisi par le dé
	private int currentNumber = 1;//nombre actuel de déplacement
	bool diceChoosed = false;




	// Update is called once per frame
	void Update()
	{
		if( _cinematique.GetCinematiqueFinished() )
		{
			_cinematique.gameObject.SetActive(false);
			_cameraManager.GetMainCamera().gameObject.SetActive(true);
			GetComponent<Animator>().SetBool("IsCinematiqueFinished", true);
		}
	}


	public EPlayer GetNextPlayer()//TODO: faire un ordre aleatoire en début de partie
	{
		switch( _currentPlayer )
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
