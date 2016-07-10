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
	private SkinnedMeshRenderer _player1;
	[SerializeField]
	private SkinnedMeshRenderer _player2;
	[SerializeField]
	private SkinnedMeshRenderer _player3;
	[SerializeField]
	private SkinnedMeshRenderer _player4;

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
			_player1.material = _captainBlue;
			break;
		case 2:
			_player2.material = _captainBlue;
			break;
		case 3:
			_player3.material = _captainBlue;
			break;
		case 4:
			_player4.material = _captainBlue;
			break;
		}
		switch( PlayerPrefs.GetInt("PLAYER_GREEN_WIN") )
		{
		case 1:
			_player1.material = _captainGreen;
			break;
		case 2:
			_player2.material = _captainGreen;
			break;
		case 3:
			_player3.material = _captainGreen;
			break;
		case 4:
			_player4.material = _captainGreen;
			break;
		}
		switch( PlayerPrefs.GetInt("PLAYER_RED_WIN") )
		{
		case 1:
			_player1.material = _captainRed;
			break;
		case 2:
			_player2.material = _captainRed;
			break;
		case 3:
			_player3.material = _captainRed;
			break;
		case 4:
			_player4.material = _captainRed;
			break;
		}
		switch( PlayerPrefs.GetInt("PLAYER_YELLOW_WIN") )
		{
		case 1:
			_player1.material = _captainYellow;
			break;
		case 2:
			_player2.material = _captainYellow;
			break;
		case 3:
			_player3.material = _captainYellow;
			break;
		case 4:
			_player4.material = _captainYellow;
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



	}

	// Update is called once per frame
	void Update()
	{

		Debug.Log(Time.timeSinceLevelLoad);
		if( Time.timeSinceLevelLoad >= 5 )
		{
			Debug.Log("YOLO");
			SceneManager.LoadScene(1);
		}

	}
}
