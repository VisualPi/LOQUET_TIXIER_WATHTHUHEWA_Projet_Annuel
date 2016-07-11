using UnityEngine;
using System.Collections.Generic;


public class AiMovement : MonoBehaviour
{
	[SerializeField]
	private PlayerMovement _playerMovement;
	[SerializeField]
	private PatternManager _patternManager;

	public List<Vector2> _currentPath;
	public int _currentIndex = 0;

	private bool ok = false;

	void Start()
	{
		_currentPath = new List<Vector2>();
	}

	public void NextMove()
	{
		if( _currentPath.Count > 0 )
		{
			var v = _currentPath[_currentIndex];
			_playerMovement.nextStep = new Vector3(v.x, _playerMovement.transform.position.y, v.y);
			_currentIndex++;
			if( _currentIndex == _currentPath.Count )
				_currentPath.Clear();
		}
		else
		{
			while( ok != true )
			{
				var r = Random.Range(1, 8);
				int h = 0, w = 0;
				if( r <= 6 )
				{
					w = Random.Range(1, 4);
				}
				r = Random.Range(1, 8);
				if( r <= 6 )
				{
					h = Random.Range(1, 4);
				}

				_currentPath = _patternManager.GenerateNewPattern(new Vector2((int)_playerMovement.nextStep.x, (int)_playerMovement.nextStep.z), h, w);
				if( _currentPath != null )
				{
					ok = true;
					break;
				}
			}
			ok = false;
			_currentIndex = 0;
			Debug.Log(_currentIndex);
			var v = _currentPath[_currentIndex];
			_playerMovement.nextStep = new Vector3(v.x, _playerMovement.transform.position.y, v.y);
			_currentIndex++;
		}

	}
}
