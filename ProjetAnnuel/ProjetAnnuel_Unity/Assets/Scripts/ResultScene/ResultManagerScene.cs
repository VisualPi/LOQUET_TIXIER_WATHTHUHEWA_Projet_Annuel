using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class ResultManagerScene : MonoBehaviour
{
	[SerializeField]
	private Material _captainBlue;
	[SerializeField]
	private Material _captainGreen;
	[SerializeField]
	private Material _captainRed;
	[SerializeField]
	private Material _captainYellow;

	[SerializeField]
	private SkinnedMeshRenderer _player1Renderer;
	[SerializeField]
	private SkinnedMeshRenderer _player2Renderer;
	[SerializeField]
	private SkinnedMeshRenderer _player3Renderer;
	[SerializeField]
	private SkinnedMeshRenderer _player4Renderer;

	[SerializeField]
	private GameObject _player1;
	[SerializeField]
	private GameObject _player2;
	[SerializeField]
	private GameObject _player3;
	[SerializeField]
	private GameObject _player4;

	[SerializeField]
	AudioClip MusicResult;

	[SerializeField]
	AudioClip ApplauseSound;

	[SerializeField]
	AudioClip EndingMusicResult;

	[SerializeField]
	Text _victoryName;

	[SerializeField]
	private GameObject _victoryCanvas;


	AudioSource audioSource;

	public bool isFinalResult;

	private void PrepareScene()
	{
        switch( PlayerPrefs.GetInt("PLAYER_BLUE_WIN") )
		{
		case 1:
			PlayerPrefs.SetInt("PLAYER_BLUE_NB_VICTORIES", PlayerPrefs.GetInt("PLAYER_BLUE_NB_VICTORIES") + 1);
			_player1Renderer.material = _captainBlue;
			break;
		case 2:
			_player2Renderer.material = _captainBlue;
			break;
		case 3:
			_player3Renderer.material = _captainBlue;
			break;
		case 4:
			_player4Renderer.material = _captainBlue;
			break;
		}
		switch( PlayerPrefs.GetInt("PLAYER_GREEN_WIN") )
		{
		case 1:
			PlayerPrefs.SetInt("PLAYER_GREEN_NB_VICTORIES", PlayerPrefs.GetInt("PLAYER_GREEN_NB_VICTORIES") + 1);
			_player1Renderer.material = _captainGreen;
			break;
		case 2:
			_player2Renderer.material = _captainGreen;
			break;
		case 3:
			_player3Renderer.material = _captainGreen;
			break;
		case 4:
			_player4Renderer.material = _captainGreen;
			break;
		}
		switch( PlayerPrefs.GetInt("PLAYER_RED_WIN") )
		{
		case 1:
			PlayerPrefs.SetInt("PLAYER_RED_NB_VICTORIES", PlayerPrefs.GetInt("PLAYER_RED_NB_VICTORIES") + 1);
			_player1Renderer.material = _captainRed;
			break;
		case 2:
			_player2Renderer.material = _captainRed;
			break;
		case 3:
			_player3Renderer.material = _captainRed;
			break;
		case 4:
			_player4Renderer.material = _captainRed;
			break;
		}
		switch( PlayerPrefs.GetInt("PLAYER_YELLOW_WIN") )
		{
		case 1:
			PlayerPrefs.SetInt("PLAYER_YELLOW_NB_VICTORIES", PlayerPrefs.GetInt("PLAYER_YELLOW_NB_VICTORIES") + 1);
			_player1Renderer.material = _captainYellow;
			break;
		case 2:
			_player2Renderer.material = _captainYellow;
			break;
		case 3:
			_player3Renderer.material = _captainYellow;
			break;
		case 4:
			_player4Renderer.material = _captainYellow;
			break;
		}

	}

	// Use this for initialization
	void Start()
	{
		PrepareScene();
        audioSource = GetComponent<AudioSource>();
		if( PlayerPrefs.GetInt("PLAYER_BLUE_NB_VICTORIES") == 5 )
		{
			Debug.Log("Blue won");
			_player1Renderer.material = _captainBlue;//tmp
			_player2Renderer.material = _captainGreen;//tmp
			_player3Renderer.material = _captainRed;//tmp
			_player4Renderer.material = _captainYellow;//tmp
			isFinalResult = true;
			_victoryName.text = PlayerPrefs.GetString("PLAYER_BLUE_NAME").ToUpper();
		}
		else if( PlayerPrefs.GetInt("PLAYER_GREEN_NB_VICTORIES") == 5 )
		{
			_player1Renderer.material = _captainGreen;//tmp
			_player2Renderer.material = _captainBlue;//tmp
			_player3Renderer.material = _captainRed;//tmp
			_player4Renderer.material = _captainYellow;//tmp
			isFinalResult = true;
			_victoryName.text = PlayerPrefs.GetString("PLAYER_GREEN_NAME").ToUpper();
		}
		else if( PlayerPrefs.GetInt("PLAYER_RED_NB_VICTORIES") == 5 )
		{
			_player1Renderer.material = _captainRed;//tmp
			_player2Renderer.material = _captainGreen;//tmp
			_player3Renderer.material = _captainBlue;//tmp
			_player4Renderer.material = _captainYellow;//tmp
			isFinalResult = true;
			_victoryName.text = PlayerPrefs.GetString("PLAYER_RED_NAME").ToUpper();
		}
		else if( PlayerPrefs.GetInt("PLAYER_YELLOW_NB_VICTORIES") == 5 )
		{
			_player1Renderer.material = _captainYellow;//tmp
			_player2Renderer.material = _captainGreen;//tmp
			_player3Renderer.material = _captainRed;//tmp
			_player4Renderer.material = _captainBlue;//tmp
			isFinalResult = true;
			_victoryName.text = PlayerPrefs.GetString("PLAYER_YELLOW_NAME").ToUpper();
		}
		if( !isFinalResult )
		{
			audioSource.PlayOneShot(MusicResult);
		}
		else
		{
			_victoryCanvas.SetActive(true);
            audioSource.PlayOneShot(EndingMusicResult);
		}
		_player1.GetComponent<Animator>().Play("victory_idle1");
		_player2.GetComponent<Animator>().Play("victory_idle2");
		_player3.GetComponent<Animator>().Play("victory_idle3");
	}

	// Update is called once per frame
	void Update()
	{
		if( Time.timeSinceLevelLoad >= 1 )
		{
			_player4.transform.LookAt(new Vector3(20f, _player4.transform.position.y, _player4.transform.position.z));
			_player4.transform.Translate(-_player4.transform.right * Time.deltaTime);
			_player4.GetComponent<Animator>().Play("sad_walk");
		}
		if( Time.timeSinceLevelLoad >= 8 )
		{
			PlayerPrefs.SetInt("PLAYER_BLUE_WIN", 0);
			PlayerPrefs.SetInt("PLAYER_GREEN_WIN", 0);
			PlayerPrefs.SetInt("PLAYER_RED_WIN", 0);
			PlayerPrefs.SetInt("PLAYER_YELLOW_WIN", 0);
			Debug.Log("YOLO");
			if( !isFinalResult )
				SceneManager.LoadScene(1);
		}

	}
}
