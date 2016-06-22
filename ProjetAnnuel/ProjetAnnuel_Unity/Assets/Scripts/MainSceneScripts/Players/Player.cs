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

	private bool isAnimPointMiddle;

	private string _name;
	private int _caseID = 0;//id de la case départ

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
	public Animator GetAnimator()
	{
		return _animator;
	}
	public void PlayAnimPointing()
	{
		_animator.SetTrigger("PointingTrigger");
	}
	public bool GetAnimPointMiddle()
	{
		return isAnimPointMiddle;
	}
	public void OnMiddleAnim()
	{
		isAnimPointMiddle = true;
	}
	public void ResetAnim()
	{
		isAnimPointMiddle = false;
	}
	public int GetCaseID()
	{
		return _caseID;
	}
	public void SetCaseID(int id)
	{
		_caseID = id;
	}
}
