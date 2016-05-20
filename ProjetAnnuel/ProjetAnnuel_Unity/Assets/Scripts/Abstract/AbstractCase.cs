using UnityEngine;
using System.Collections;
//public enum ECaseType { DEPART, FIN, BLUE, RED, GREEN, YELLOW }
public abstract class AbstractCase : MonoBehaviour
{
	[SerializeField]
	private AbstractCase	_previousCase;
	[SerializeField]
	private AbstractCase	_nextCase;
	[SerializeField]
	private MeshRenderer    _meshRenderer;
	[SerializeField]
	private Transform       _transform;

	private int _nbPlayerOnCase;


	public AbstractCase GetNextCase()
	{
		return _nextCase;
	}
	public void SetNextCase( AbstractCase value )
	{
		_nextCase = value;
	}
	public AbstractCase GetPreviousCase()
	{
		return _previousCase;
	}
	public void SetPreviousCase( AbstractCase value )
	{
		_previousCase = value;
	}
	public MeshRenderer GetMeshRenderer()
	{
		return _meshRenderer;
	}
	public Transform GetTransform()
	{
		return _transform;
	}
	public int GetNbPlayerOnCase()
	{
		return _nbPlayerOnCase;
	}
	public void SetNbPlayerOnCase(int value)
	{
		_nbPlayerOnCase = value;
	}

}
