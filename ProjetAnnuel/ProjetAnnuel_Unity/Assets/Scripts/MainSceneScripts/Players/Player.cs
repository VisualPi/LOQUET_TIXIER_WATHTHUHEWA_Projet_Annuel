using UnityEngine;
using System.Collections;

public enum EPlayer { BLUE = 0, GREEN, RED, YELLOW };

public class Player : MonoBehaviour
{
	[SerializeField]
	private bool _isAI;
	[SerializeField]
	private EPlayer _playerColor;
	[SerializeField]
	private Animator _animator;

	private Vector3 startMarker;
	private Vector3 endMarker;
	private float speed = 30.0f;
	private float startTime;
	private float animLength;

	private bool animStarted;
	private bool isPositionReached;

	private string _name;

	public void Update()
	{
		if( animStarted )
		{
			_animator.Play("walking");
			float distCovered = (Time.time - startTime) * speed;
			float fracJourney = distCovered / animLength;
			transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
			transform.LookAt(endMarker);
			if( Mathf.Abs(transform.position.x - endMarker.x) < 0.01f && Mathf.Abs(transform.position.y - endMarker.y) < 0.01f )
			{
				//_animator.Stop();
				_animator.Play("idle");
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
	public void SetName( string value)
	{
		_name = value;
	}
	public string GetName()
	{
		return _name;
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
		isPositionReached = false;
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
	public void PlayAnimPointing()
	{
		_animator.SetBool("isPointing", true);
	}
	private bool isAnimPointEnded;
	private bool isAnimPointMiddle;
	public bool GetAnimPointEnded()
	{
		return isAnimPointEnded;
	}
	public bool GetAnimPointMiddle()
	{
		return isAnimPointMiddle;
	}
	public void OnAnimPointingEnd()
	{
		_animator.Play("idle");
		_animator.SetBool("isPointing", false);
		isAnimPointEnded = true;
    }
	public void PauseAnim()
	{
		isAnimPointMiddle = true;
		_animator.speed = 0.5f;
	}
	public void ResumeAnim()
	{
		_animator.speed =1f;
		isAnimPointMiddle = false;
		isAnimPointEnded = false;

    }
}
