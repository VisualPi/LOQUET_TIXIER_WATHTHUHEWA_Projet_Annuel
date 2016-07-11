using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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


	AudioSource audioSource;

	public bool isFinalResult;

	private void PrepareScene()
	{
		switch( PlayerPrefs.GetInt("PLAYER_BLUE_WIN") )
		{
		case 1:
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

		if( !isFinalResult )
		{
			audioSource.PlayOneShot(MusicResult);
		}
		else
		{
			audioSource.PlayOneShot(EndingMusicResult);
		}
		_player1.GetComponent<Animator>().Play("victory_idle1");
		_player2.GetComponent<Animator>().Play("victory_idle2");
		_player3.GetComponent<Animator>().Play("victory_idle3");
	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log(Time.timeSinceLevelLoad);
		if( Time.timeSinceLevelLoad >= 1 )
		{
			_player4.transform.LookAt(new Vector3(20f, _player4.transform.position.y, _player4.transform.position.z));
			_player4.transform.Translate(-_player4.transform.right* Time.deltaTime);
			_player4.GetComponent<Animator>().Play("sad_walk");
		}
		if( Time.timeSinceLevelLoad >= 8 )
		{
			Debug.Log("YOLO");
			SceneManager.LoadScene(1);
		}

	}
}
