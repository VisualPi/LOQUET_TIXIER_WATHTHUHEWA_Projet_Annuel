using UnityEngine;
using System.Collections;

//public enum ECaseType { DEPART, FIN, BLUE, RED, GREEN, YELLOW }

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
			_meshRenderer.material = Utils.Instance.purple;
			break;
		case ECaseType.FIN:
			_meshRenderer.material = Utils.Instance.black;
			break;
		case ECaseType.BLUE:
			_meshRenderer.material = Utils.Instance.blue;
			break;
		case ECaseType.RED:
			_meshRenderer.material = Utils.Instance.red;
			break;
		case ECaseType.GREEN:
			_meshRenderer.material = Utils.Instance.green;
			break;
		case ECaseType.YELLOW:
			_meshRenderer.material = Utils.Instance.yellow;
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

	#endregion

}
