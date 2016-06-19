using UnityEngine;
using System.Collections;

public enum EPlayer { BLUE = 0, GREEN, RED, YELLOW };

public class Player : MonoBehaviour
{
	[SerializeField]
	private bool _isAI;
	[SerializeField]
	private EPlayer _playerColor;


	private Vector3 startMarker;
	private Vector3 endMarker;
	private float speed = 15.0F;
	private float startTime;
	private float animLength;

	private bool animStarted;
	private bool isPositionReached;

	public void Update()
	{
		if( animStarted )
		{
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / animLength;
			transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
			if( Mathf.Abs(transform.position.x - endMarker.x) < 0.1f && Mathf.Abs(transform.position.y - endMarker.y) < 0.1f )
			{
				isPositionReached = true;
			}
		}
	}

	public void SetIsAI( bool value )
	{
		_isAI = value;
	}
	public bool GetIsAI()
	{
		return _isAI;
	}
	public EPlayer GetPlayerColor()
	{
		return _playerColor;
	}

	public void InitAnim( Vector3 endMarker )
	{
		startTime = Time.time;
		startMarker = transform.position;
		this.endMarker = endMarker;
		animLength = Vector3.Distance(startMarker, endMarker);
		animStarted = true;
	}

	public bool IsPositionReached()
	{
		return isPositionReached;
	}
	public bool IsAnimStarted()
	{
		return animStarted;
	}
	public void SetAnimStarted(bool value)
	{
		animStarted = value;
	}
}
