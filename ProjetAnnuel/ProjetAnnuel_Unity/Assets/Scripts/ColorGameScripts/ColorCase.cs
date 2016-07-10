using UnityEngine;
using System.Collections;

public enum EColorCaseType { DEFAULT, BLUE, GREEN, RED, YELLOW }

public class ColorCase : MonoBehaviour
{
	[SerializeField]
	private EColorCaseType _type = EColorCaseType.DEFAULT;

	[SerializeField]
	private BoxCollider _collider;

	[SerializeField]
	private MeshRenderer _meshRenderer;

	private Material _defaultMat;

	public void Start()
	{
		_defaultMat = _meshRenderer.material;
		switch( _type )
		{
		case EColorCaseType.DEFAULT:
			// _meshRenderer.material = Utils.Instance.purple;
			break;
		case EColorCaseType.BLUE:
			_meshRenderer.material = Utils.Instance.blue;
			break;
		case EColorCaseType.GREEN:
			_meshRenderer.material = Utils.Instance.green;
			break;
		case EColorCaseType.RED:
			_meshRenderer.material = Utils.Instance.red;
			break;
		case EColorCaseType.YELLOW:
			_meshRenderer.material = Utils.Instance.yellow;
			break;
		default:
			return;
		}
	}

	private void ConvertColor( EPlayer player )
	{
		switch( player )
		{
		case EPlayer.BLUE:
			_type = EColorCaseType.BLUE;
			_meshRenderer.material = Utils.Instance.blue;
			break;
		case EPlayer.GREEN:
			_type = EColorCaseType.GREEN;
			_meshRenderer.material = Utils.Instance.green;
			break;
		case EPlayer.RED:
			_type = EColorCaseType.RED;
			_meshRenderer.material = Utils.Instance.red;
			break;
		case EPlayer.YELLOW:
			_type = EColorCaseType.YELLOW;
			_meshRenderer.material = Utils.Instance.yellow;
			break;
		}
	}
	public void OnTriggerEnter( Collider col )
	{
		ConvertColor(col.gameObject.GetComponent<Player>().GetPlayerColor());
	}

	public EColorCaseType GetColor()
	{
		return _type;
	}

	public void SetColor( EColorCaseType type )
	{
		switch( type )
		{
		case EColorCaseType.DEFAULT:
			_type = EColorCaseType.DEFAULT;
			_meshRenderer.material = _defaultMat;
			break;
		case EColorCaseType.BLUE:
			_type = EColorCaseType.BLUE;
			_meshRenderer.material = Utils.Instance.blue;
			break;
		case EColorCaseType.GREEN:
			_type = EColorCaseType.GREEN;
			_meshRenderer.material = Utils.Instance.green;
			break;
		case EColorCaseType.RED:
			_type = EColorCaseType.RED;
			_meshRenderer.material = Utils.Instance.red;
			break;
		case EColorCaseType.YELLOW:
			_type = EColorCaseType.YELLOW;
			_meshRenderer.material = Utils.Instance.yellow;
			break;
		}
	}
}
