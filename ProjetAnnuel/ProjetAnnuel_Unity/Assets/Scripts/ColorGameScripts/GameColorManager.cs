using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameColorManager : MonoBehaviour
{
	[SerializeField]
	private List<Transform> _cases;

	[SerializeField]
	private List<ColorCase> _colorCases;

	private Vector2[][] _logicalCases;
	private int[][] _tmpLogicalCases;

	[SerializeField]
	private Text _scoreBlue;
	[SerializeField]
	private Text _scoreGreen;
	[SerializeField]
	private Text _scoreRed;
	[SerializeField]
	private Text _scoreYellow;
	[SerializeField]
	private Text _blueWinner;
	[SerializeField]
	private Text _greenWinner;
	[SerializeField]
	private Text _redWinner;
	[SerializeField]
	private Text _yellowWinner;
	[SerializeField]
	private Text _chrono;

	[SerializeField]
	private GameObject _three;
	[SerializeField]
	private GameObject _two;
	[SerializeField]
	private GameObject _one;

	[SerializeField]
	private AudioSource _audioSource;
	[SerializeField]
	private AudioClip _timer;
	[SerializeField]
	private AudioClip _timerEnd;

	[SerializeField]
	private AudioSource _audioSourceAmbient;
	[SerializeField]
	private AudioClip _crashBandicoot2;

	[SerializeField]
	private AudioClip _kaching;


	void Start()
	{
		_logicalCases = new Vector2[10][];
		for( var i = 0 ; i < 10 ; i++ )
			_logicalCases[i] = new Vector2[10];
		for( var i = 0 ; i < 10 ; i++ )
			for( var j = 0 ; j < 10 ; j++ )
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

	private void InitTmpLogicalCase()
	{
		_tmpLogicalCases = new int[10][];
		for( var i = 0 ; i < 10 ; i++ )
			_tmpLogicalCases[i] = new int[10];
		for( var i = 0 ; i < 10 ; i++ )
			for( var j = 0 ; j < 10 ; j++ )
				_tmpLogicalCases[i][j] = 0;
	}

	public bool IsCaseByPos( Vector2 position )//C'est un vector2 pour etre plus pratique mais on test bien x et z
	{
		foreach( var c in _cases )
		{
			if( c.transform.position.x == (int)position.x && c.transform.position.z == (int)position.y )
				return true;
		}
		return false;
	}

	public ColorCase GetCaseByPos( Vector3 position )
	{
		foreach( var c in _colorCases )
		{
			if( c.transform.position.x == (int)position.x && c.transform.position.z == (int)position.z )
				return c;
		}
		return null;
	}
	public ColorCase GetCaseByPos( Vector2 position )
	{
		foreach( var c in _colorCases )
		{
			if( c.transform.position.x == (int)position.x && c.transform.position.z == (int)position.y )
				return c;
		}
		return null;
	}

	private bool ComparePos( Vector3 pos1, Vector3 pos2 )
	{
		return Mathf.Abs(pos1.x - pos2.x) <= 0.1f && Mathf.Abs(pos1.z - pos2.z) <= 0.1f;
	}

	private bool HasPlayerOnCase( EPlayer player, Vector3 pos )
	{
		switch( player )
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
		for( var i = 0 ; i < 4 ; i++ )
		{
			var pos = Utils.Instance.GetPlayerByColor((EPlayer)i).GetComponent<PlayerMovement>().nextStep;
			var pPos = Utils.Instance.GetPlayerByColor((EPlayer)i).GetComponent<PlayerMovement>().defaultPos;
			if( pos.x != pPos.x && pos.z != pPos.z )
			{
				var c = IsCaseByPos(new Vector2(pos.x, pos.z));
				if( c && !HasPlayerOnCase((EPlayer)i, pos) )
				{
					Utils.Instance.GetPlayerByColor((EPlayer)i).GetComponent<PlayerMovement>().Move(pos);
					//Utils.Instance.GetPlayerByColor((EPlayer)i).transform.position = pos;
				}

			}
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
		_scoreBlue.text = _bluePoints.ToString();
		_scoreGreen.text = _greenPoints.ToString();
		_scoreRed.text = _redPoints.ToString();
		_scoreYellow.text = _yellowPoints.ToString();
	}

	private List<Vector2> GetNeighboors( Vector2 coord )
	{
		List<Vector2> n = new List<Vector2>();
		var pos = new Vector2(coord.x + 5f, coord.y);
		if( GetCaseByPos(pos) != null )
			n.Add(pos);
		pos = new Vector2(coord.x - 5f, coord.y);
		if( GetCaseByPos(pos) != null )
			n.Add(pos);
		pos = new Vector2(coord.x, coord.y + 5f);
		if( GetCaseByPos(pos) != null )
			n.Add(pos);
		pos = new Vector2(coord.x, coord.y - 5f);
		if( GetCaseByPos(pos) != null )
			n.Add(pos);
		return n;
	}

	private void FillTmpLogicalCases( EColorCaseType color )
	{
		InitTmpLogicalCase();
		for( var y = 0 ; y < 10 ; y++ )
		{
			for( var x = 0 ; x < 10 ; x++ )
			{
				if( GetCaseByPos(_logicalCases[x][y]).GetColor() == color )
				{
					_tmpLogicalCases[x][y] = 1;
				}
			}
		}
	}

	private List<EColorCaseType> _toRes = new List<EColorCaseType>();

	private void CheckForSquares()
	{
		for( var i = 1 ; i < 5 ; i++ )//de BLUE a YELLOW dans EColorCaseType
		{
			FillTmpLogicalCases((EColorCaseType)i);
			for( var y = 0 ; y < 10 ; y++ )
			{
				for( var x = 0 ; x < 10 ; x++ )
				{
					if( _tmpLogicalCases[x][y] == 0 )
					{
						CheckGoodSquare(_logicalCases[x][y]);
						if( ok )
						{
							ok = true;
							FillCurrentSquare(_logicalCases[x][y], (EColorCaseType)i);
							GetPoints((EColorCaseType)i);
							if (! _toRes.Contains((EColorCaseType)i) )
								_toRes.Add((EColorCaseType)i);
							//GetPointAndRes((EColorCaseType)i);
							x = 10;
							y = 10;
							break;
						}
						ok = true;
					}
				}
			}
		}
	}

	private bool IsOnBorder( Vector2 coord )
	{
		var pos = new Vector2(coord.x + 5f, coord.y);
		if( GetCaseByPos(pos) == null )
			return true;
		pos = new Vector2(coord.x - 5f, coord.y);
		if( GetCaseByPos(pos) == null )
			return true;
		pos = new Vector2(coord.x, coord.y + 5f);
		if( GetCaseByPos(pos) == null )
			return true;
		pos = new Vector2(coord.x, coord.y - 5f);
		if( GetCaseByPos(pos) == null )
			return true;
		return false;
	}

	private int[] GetLogicalCoord( Vector2 coord )
	{
		var ret = new int[2];
		for( var y = 0 ; y < 10 ; y++ )
		{
			for( var x = 0 ; x < 10 ; x++ )
			{
				if( _logicalCases[x][y].x == coord.x && _logicalCases[x][y].y == coord.y )
				{
					ret[0] = x;
					ret[1] = y;
					return ret;
				}
			}
		}
		return null;
	}

	private bool ok = true;

	private void CheckGoodSquare( Vector2 coord )
	{
		if( ok )
		{
			if( IsOnBorder(coord) )
			{
				ok = false;
			}

		}
		var c = GetLogicalCoord(coord);
		_tmpLogicalCases[c[0]][c[1]] = 2;
		foreach( var n in GetNeighboors(coord) )
		{
			var tmp = GetLogicalCoord(n);
			if( _tmpLogicalCases[tmp[0]][tmp[1]] == 0 )
				CheckGoodSquare(n);
		}
	}

	private void FillCurrentSquare( Vector2 coord, EColorCaseType color )
	{
		List<Vector2> tmp = new List<Vector2>();
		tmp.Add(coord);
		foreach( var n in GetNeighboors(coord) )
		{
			tmp.Add(n);
		}
		foreach( var t in tmp )
		{
			var n = GetLogicalCoord(t);
			if( _tmpLogicalCases[n[0]][n[1]] == 2 )
			{
				var c = GetLogicalCoord(t);
				GetCaseByPos(t).SetColor(color);
			}
		}

	}

	private void GetWinner()
	{
		List<int> tmp = new List<int>() { _bluePoints, _greenPoints, _redPoints, _yellowPoints };
		tmp.Sort();
		//FIRST
		if( tmp[3] == _bluePoints )
		{
			PlayerPrefs.SetInt("PLAYER_BLUE_WIN", 1);
			_blueWinner.gameObject.SetActive(true);
		}
		if( tmp[3] == _greenPoints )
		{
			PlayerPrefs.SetInt("PLAYER_GREEN_WIN", 1);
			_greenWinner.gameObject.SetActive(true);
		}
		if( tmp[3] == _redPoints )
		{
			PlayerPrefs.SetInt("PLAYER_RED_WIN", 1);
			_redWinner.gameObject.SetActive(true);
		}
		if( tmp[3] == _yellowPoints )
		{
			PlayerPrefs.SetInt("PLAYER_YELLOW_WIN", 1);
			_yellowWinner.gameObject.SetActive(true);
		}
		//TWO
		if( tmp[2] == _bluePoints )
		{
			PlayerPrefs.SetInt("PLAYER_BLUE_WIN", 2);
		}
		if( tmp[2] == _greenPoints )
		{
			PlayerPrefs.SetInt("PLAYER_GREEN_WIN", 2);
		}
		if( tmp[2] == _redPoints )
		{
			PlayerPrefs.SetInt("PLAYER_RED_WIN", 2);
		}
		if( tmp[2] == _yellowPoints )
		{
			PlayerPrefs.SetInt("PLAYER_YELLOW_WIN", 2);
		}
		//THREE
		if( tmp[1] == _bluePoints )
		{
			PlayerPrefs.SetInt("PLAYER_BLUE_WIN", 3);
		}
		if( tmp[1] == _greenPoints )
		{
			PlayerPrefs.SetInt("PLAYER_GREEN_WIN", 3);
		}
		if( tmp[1] == _redPoints )
		{
			PlayerPrefs.SetInt("PLAYER_RED_WIN", 3);
		}
		if( tmp[1] == _yellowPoints )
		{
			PlayerPrefs.SetInt("PLAYER_YELLOW_WIN", 3);
		}
		//FOUR
		if( tmp[0] == _bluePoints )
		{
			PlayerPrefs.SetInt("PLAYER_BLUE_WIN", 4);
		}
		if( tmp[0] == _greenPoints )
		{
			PlayerPrefs.SetInt("PLAYER_GREEN_WIN", 4);
		}
		if( tmp[0] == _redPoints )
		{
			PlayerPrefs.SetInt("PLAYER_RED_WIN", 4);
		}
		if( tmp[0] == _yellowPoints )
		{
			PlayerPrefs.SetInt("PLAYER_YELLOW_WIN", 4);
		}
	}

	private void Reset( EColorCaseType type )
	{
		switch( type )
		{
		case EColorCaseType.BLUE:
			EmptyCases(type);
			_audioSource.PlayOneShot(_kaching);
			break;

		}
	}

	private void GetPoints( EColorCaseType type )
	{
		var pts = 0;
		foreach( var c in _colorCases )
		{
			if( c.GetColor() == type )
				pts++;
		}
		switch (type)
		{
		case EColorCaseType.BLUE:
			_bluePoints = pts;
			break;
		case EColorCaseType.GREEN:
			_greenPoints = pts;
			break;
		case EColorCaseType.RED:
			_redPoints = pts;
			break;
		case EColorCaseType.YELLOW:
			_yellowPoints = pts;
			break;
		}
	}

	private void EmptyCases( EColorCaseType type )
	{
		foreach( var c in _colorCases )
		{
			if( c.GetColor() == type )
				c.SetColor(EColorCaseType.DEFAULT);
		}
	}


	private void EmptyAllCases()
	{
		foreach( var c in _colorCases )
		{
			c.SetColor(EColorCaseType.DEFAULT);
		}
	}

	[SerializeField]
	private bool checkForSquare;

	[SerializeField]
	private bool _debut;
	private float _timePerLetter = 1f;
	void Update()
	{
		if( _debut )
		{
			StartCoroutine(Countdown());
		}
		if( _gameStarted )
		{

			_timeGameEllapsed += Time.deltaTime;
			_timeLapEllapsed += Time.deltaTime;
			_timePerStepEllapsed += Time.deltaTime;
			_chrono.text = ( _gameDuration - (int)_timeGameEllapsed ).ToString();
			if( _timeGameEllapsed >= _gameDuration )
			{
				_gameStarted = false;
				_timeGameEllapsed = 0f;
				GetWinner();
				SceneManager.LoadScene("result");
			}
			if( _timeGameEllapsed >= ( _gameDuration - 10f ) )
			{
				_chrono.color = Color.red;
			}
			if( _timePerStepEllapsed >= _timePerStep )
			{
				if(_toRes.Count > 0)
				{
					foreach (var t in _toRes)
					{
						Reset(t);
                    }
					_toRes.Clear();
				}
				MovePlayers();
				_timePerStepEllapsed = 0f;
			}
			GetPoints();
			CheckForSquares();
		}
	}

	IEnumerator Countdown()
	{
		_debut = false;
		Debug.Log("first");
		_audioSource.PlayOneShot(_timer);
		_three.SetActive(true);
		yield return new WaitForSeconds(1);
		Debug.Log("second");
		_audioSource.PlayOneShot(_timer);
		_three.SetActive(false);
		_two.SetActive(true);
		yield return new WaitForSeconds(1);
		Debug.Log("third");
		_audioSource.PlayOneShot(_timer);
		_two.SetActive(false);
		_one.SetActive(true);
		yield return new WaitForSeconds(1);
		Debug.Log("last");
		_audioSource.PlayOneShot(_timerEnd);
		_one.SetActive(false);
		_gameStarted = true;
		_chrono.gameObject.SetActive(true);
		_audioSourceAmbient.PlayOneShot(_crashBandicoot2);
		yield return new WaitForSeconds(1);
	}
}
