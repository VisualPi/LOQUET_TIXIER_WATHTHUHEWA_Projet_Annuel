using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	private List<EPlayer> _playerOrder;

	private EPlayer _currentPlayer;

	[SerializeField]
	private CameraManager _cameraManager;

	[SerializeField]
	private Cinematique _cinematique;

	[SerializeField]
	AudioSource audioSource;
	[SerializeField]
	AudioSource audioSource2;

	[SerializeField]
	AudioClip music;

	[SerializeField]
	AudioClip intro;

	[SerializeField]
	AudioClip wavesound;

	[SerializeField]
	private GameObject _canvasCinematique;

	[SerializeField]
	private Text _blueName;
	[SerializeField]
	private Text _greenName;
	[SerializeField]
	private Text _redName;
	[SerializeField]
	private Text _yellowName;

	[SerializeField]
	private Image _blueHead;
	[SerializeField]
	private Image _greenHead;
	[SerializeField]
	private Image _redHead;
	[SerializeField]
	private Image _yellowHead;


	void Start()
	{
		//TMP



		//PlayerPrefs.SetString("PLAYER_BLUE_NAME", "NONAME1");
		//PlayerPrefs.SetString("PLAYER_GREEN_NAME", "NONAME2");
		//PlayerPrefs.SetString("PLAYER_RED_NAME", "NONAME3");
		//PlayerPrefs.SetString("PLAYER_YELLOW_NAME", "NONAME4");
		//
		//PlayerPrefs.SetInt("PLAYER_BLUE_ISAI", 1);
		//PlayerPrefs.SetInt("PLAYER_GREEN_ISAI", 1);
		//PlayerPrefs.SetInt("PLAYER_RED_ISAI", 1);
		//PlayerPrefs.SetInt("PLAYER_YELLOW_ISAI", 1);
		//
		//PlayerPrefs.SetInt("PLAYER_BLUE_STARS", 0);
		//PlayerPrefs.SetInt("PLAYER_GREEN_STARS", 0);
		//PlayerPrefs.SetInt("PLAYER_RED_STARS", 0);
		//PlayerPrefs.SetInt("PLAYER_YELLOW_STARS", 0);
		//
		//* PlayerPrefs.SetInt("PLAYER_BLUE_CASEID", 0);
		//PlayerPrefs.SetInt("PLAYER_GREEN_CASEID", 0);
		//PlayerPrefs.SetInt("PLAYER_RED_CASEID", 0);
		//PlayerPrefs.SetInt("PLAYER_YELLOW_CASEID", 0);*/
		//
		//PlayerPrefs.SetInt("GAME_ISFIRSTRUN", 0);
		//
		////TMP







		_playerOrder = new List<EPlayer>(4);
		Utils.Instance.GetPlayerByColor(EPlayer.BLUE).SetName(PlayerPrefs.GetString("PLAYER_BLUE_NAME"));
		Utils.Instance.GetPlayerByColor(EPlayer.GREEN).SetName(PlayerPrefs.GetString("PLAYER_GREEN_NAME"));
		Utils.Instance.GetPlayerByColor(EPlayer.RED).SetName(PlayerPrefs.GetString("PLAYER_RED_NAME"));
		Utils.Instance.GetPlayerByColor(EPlayer.YELLOW).SetName(PlayerPrefs.GetString("PLAYER_YELLOW_NAME"));
		Utils.Instance.GetPlayerByColor(EPlayer.BLUE).SetIsAI(PlayerPrefs.GetInt("PLAYER_BLUE_ISAI") == 1 ? true : false);
		Utils.Instance.GetPlayerByColor(EPlayer.GREEN).SetIsAI(PlayerPrefs.GetInt("PLAYER_GREEN_ISAI") == 1 ? true : false);
		Utils.Instance.GetPlayerByColor(EPlayer.RED).SetIsAI(PlayerPrefs.GetInt("PLAYER_RED_ISAI") == 1 ? true : false);
		Utils.Instance.GetPlayerByColor(EPlayer.YELLOW).SetIsAI(PlayerPrefs.GetInt("PLAYER_YELLOW_ISAI") == 1 ? true : false);


		if( PlayerPrefs.GetInt("GAME_ISFIRSTRUN") == 1 ) //si jamais c'est le premier lancement de la scene on met GAME_ISFIRSTRUN a 0
		{
			StartCoroutine(PlaySounds()); //appel de la coroutine qui lance le son de l'intro et apres le son du plateau
			PlayerPrefs.SetInt("GAME_ISFIRSTRUN", 0);
			//on place les joueurs sur la case départ
			Utils.Instance.GetPlayerByColor(EPlayer.BLUE).transform.position = Utils.Instance.GetDepartCase().GetCasePosition(EPlayer.BLUE, false);
			Utils.Instance.GetPlayerByColor(EPlayer.GREEN).transform.position = Utils.Instance.GetDepartCase().GetCasePosition(EPlayer.GREEN, false);
			Utils.Instance.GetPlayerByColor(EPlayer.RED).transform.position = Utils.Instance.GetDepartCase().GetCasePosition(EPlayer.RED, false);
			Utils.Instance.GetPlayerByColor(EPlayer.YELLOW).transform.position = Utils.Instance.GetDepartCase().GetCasePosition(EPlayer.YELLOW, false);
			Utils.Instance.GetPlayerByColor(EPlayer.BLUE).SetCaseID(0);
			Utils.Instance.GetPlayerByColor(EPlayer.GREEN).SetCaseID(0);
			Utils.Instance.GetPlayerByColor(EPlayer.RED).SetCaseID(0);
			Utils.Instance.GetPlayerByColor(EPlayer.YELLOW).SetCaseID(0);
		}
		else if( PlayerPrefs.GetInt("GAME_ISFIRSTRUN") == 0 )//si ce n'est pas le premier lancement de la scene on lance pas la cinématique
		{
			audioSource2.PlayOneShot(music);
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

	private bool _playSound = false;

	// Update is called once per frame
	void Update()
	{
		if( BLA )
			SceneManager.LoadScene("main");
		if( _cinematique.GetCinematiqueFinished() )
		{
			_canvasCinematique.SetActive(false);
			_cinematique.gameObject.SetActive(false);
			_cameraManager.GetMainCamera().gameObject.SetActive(true);
			GetComponent<Animator>().SetBool("IsCinematiqueFinished", true);
		}
	}

	private IEnumerator PlaySounds()
	{
		audioSource.PlayOneShot(intro);
		yield return new WaitForSeconds(1);
		while( !_cinematique.GetCinematiqueFinished() )
		{
			if( Time.timeSinceLevelLoad >= 6 )
			{
				StartCoroutine(DisplayPlayers());
				_canvasCinematique.SetActive(true);

			}
			audioSource.volume -= 0.05f;
			yield return new WaitForSeconds(1);
		}
		audioSource2.PlayOneShot(music);
		yield return new WaitForSeconds(1);
		audioSource.Stop();
	}

	private IEnumerator DisplayPlayers()
	{
		while( !_cinematique.GetCinematiqueFinished() )
		{
			_blueName.text = PlayerPrefs.GetString("PLAYER_BLUE_NAME").ToUpper();
			_greenName.text = PlayerPrefs.GetString("PLAYER_GREEN_NAME").ToUpper();
			_redName.text = PlayerPrefs.GetString("PLAYER_RED_NAME").ToUpper();
			_yellowName.text = PlayerPrefs.GetString("PLAYER_YELLOW_NAME").ToUpper();
			_blueName.color = new Color(_blueName.color.r, _blueName.color.g, _blueName.color.b, _blueName.color.a + 0.02f);
			_greenName.color = new Color(_greenName.color.r, _greenName.color.g, _greenName.color.b, _greenName.color.a + 0.02f);
			_redName.color = new Color(_redName.color.r, _redName.color.g, _redName.color.b, _redName.color.a + 0.02f);
			_yellowName.color = new Color(_yellowName.color.r, _yellowName.color.g, _yellowName.color.b, _yellowName.color.a + 0.02f);

			_blueHead.color = new Color(_blueHead.color.r, _blueHead.color.g, _blueHead.color.b, _blueHead.color.a + 0.02f);
			_greenHead.color = new Color(_greenHead.color.r, _greenHead.color.g, _greenHead.color.b, _greenHead.color.a + 0.02f);
			_redHead.color = new Color(_redHead.color.r, _redHead.color.g, _redHead.color.b, _redHead.color.a + 0.02f);
			_yellowHead.color = new Color(_yellowHead.color.r, _yellowHead.color.g, _yellowHead.color.b, _yellowHead.color.a + 0.02f);
			yield return new WaitForSeconds(0.2f);
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
	public void SetDiceNumber( int number )
	{
		diceNumber = number;
	}
	public int GetDiceNumber()
	{
		return diceNumber;
	}
}
