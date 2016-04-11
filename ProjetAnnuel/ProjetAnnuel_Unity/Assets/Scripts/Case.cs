using UnityEngine;
using System.Collections;

public enum ECaseType { DEPART, FIN, BLUE, RED, GREEN, YELLOW }

public class Case : MonoBehaviour
{
	[SerializeField]
	private Case            _nextCase;
	[SerializeField]
	private ECaseType       _caseType;
	[SerializeField]
	private MeshRenderer    _meshRenderer;
	[SerializeField]
	private Transform       _transform;

	private int _nbPlayerOnCase;


	//Init la couleur de chaque case en fonction de leur types. Set le nombre de joueur a 0 sauf pour la case de départ
	void Start()
	{
		switch( _caseType )
		{
		case ECaseType.DEPART:
			_meshRenderer.material = Resources.Load("Materials/Case/Purple", typeof(Material)) as Material;
			break;
		case ECaseType.FIN:
			_meshRenderer.material = Resources.Load("Materials/Case/Black", typeof(Material)) as Material;
			break;
		case ECaseType.BLUE:
			_meshRenderer.material = Resources.Load("Materials/Case/Blue", typeof(Material)) as Material;
			break;
		case ECaseType.RED:
			_meshRenderer.material = Resources.Load("Materials/Case/Red", typeof(Material)) as Material;
			break;
		case ECaseType.GREEN:
			_meshRenderer.material = Resources.Load("Materials/Case/Green", typeof(Material)) as Material;
			break;
		case ECaseType.YELLOW:
			_meshRenderer.material = Resources.Load("Materials/Case/Yellow", typeof(Material)) as Material;
			break;
		}
		if( _caseType != ECaseType.DEPART )
			_nbPlayerOnCase = 0;
		else
			_nbPlayerOnCase = 4;
	}

	void Update()
	{

	}

	#region Getter/Setter
	public int GetNbPlayerOnCase()
	{
		return _nbPlayerOnCase;
	}
	public void SetNbPlayerOnCase( int value )
	{
		_nbPlayerOnCase = value;
	}
	public Case GetPreviousCase()
	{
		return _previousCase;
	}
	public void SetPreviousCase( Case value )
	{
		_previousCase = value;
	}
	public Case GetNextCase()
	{
		return _nextCase;
	}
	public void SetNextCase( Case value )
	{
		_nextCase = value;
	}
	#endregion

}
