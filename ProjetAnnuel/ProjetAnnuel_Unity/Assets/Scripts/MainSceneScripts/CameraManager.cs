using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
	[SerializeField]
	private Camera _mainCamera;
	[SerializeField]
	private Camera _playerCamera;
	[SerializeField]
	private Canvas _playerCanvas;


	[SerializeField]
	private TextAnimation _nameText;
	[SerializeField]
	private TextAnimation _turnText;

	[SerializeField]
	private Roulette _roulette;

	private EPlayer currentPlayer;

	private int number;

	public void FocusOnPlayer( EPlayer player, float percentage )
	{
		
		_mainCamera.gameObject.SetActive(false);
		_mainCamera.GetComponent<AudioListener>().enabled = false;
		_playerCamera.gameObject.SetActive(true);
		_playerCamera.GetComponent<AudioListener>().enabled = true;
		_playerCanvas.gameObject.SetActive(true);
		var pos = Utils.Instance.GetPlayerByColor(player).transform.position;
		var rot = Utils.Instance.GetPlayerByColor(player).transform.rotation;
		_playerCamera.gameObject.transform.position = Vector3.Lerp(_mainCamera.gameObject.transform.position, new Vector3(pos.x + 4, pos.y + 5, pos.z - 15), percentage);
		_playerCamera.gameObject.transform.rotation = Quaternion.Lerp(_playerCamera.gameObject.transform.rotation, new Quaternion(rot.x + 4, rot.y + 5, rot.z - 15, rot.w), percentage);
	}
	public void UnfocusOnPlayer( EPlayer player )
	{
		_mainCamera.gameObject.SetActive(true);
		_mainCamera.GetComponent<AudioListener>().enabled = true;
		_playerCamera.gameObject.SetActive(false);
		_playerCamera.GetComponent<AudioListener>().enabled = false;
		_playerCanvas.gameObject.SetActive(false);
	}

	public TextAnimation GetNameText()
	{
		return _nameText;
	}
	public TextAnimation GetTurnText()
	{
		return _turnText;
	}

	public Roulette GetRoulette()
	{
		return _roulette;
	}
	public GameObject GetMainCamera()
	{
		return _mainCamera.gameObject;
	}
	public GameObject GetPlayerCamera()
	{
		return _playerCamera.gameObject;
	}
	public GameObject GetPlayerCanvas()
	{
		return _playerCanvas.gameObject;
	}

}
