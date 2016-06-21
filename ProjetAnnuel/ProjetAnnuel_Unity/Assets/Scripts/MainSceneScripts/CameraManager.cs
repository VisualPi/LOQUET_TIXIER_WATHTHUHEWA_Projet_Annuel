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

	private bool isFocused;
	private bool isProcessFinished;
	private bool isWaitForAnim;

	private EPlayer currentPlayer;

	private int number;

	void Update()
	{
		if( isWaitForAnim )
		{
			if( Utils.Instance.GetPlayerByColor(currentPlayer).GetAnimPointEnded() )
			{
				isProcessFinished = true;
			}
		
		}
		else if( isFocused )
		{
			if( !_nameText.GetAnimFinished() )
				_nameText.PlayAnim();
			else
			{
				if( !_turnText.GetAnimFinished() )
					_turnText.PlayAnim();
				else
				{
					if( !Utils.Instance.GetPlayerByColor(currentPlayer).GetAnimPointMiddle() )
						Utils.Instance.GetPlayerByColor(currentPlayer).PlayAnimPointing();
					else
					{
						number = Random.Range(1, 9);
						_roulette.StopSpin(number);
						isWaitForAnim = true;
					}
				}

			}
		}

	}

	public void FocusOnPlayer( EPlayer player )
	{
		currentPlayer = player;
		_nameText.SetText(Utils.Instance.GetPlayerByColor(player).GetName().ToUpper());
		var pos = Utils.Instance.GetPlayerByColor(player).transform.position;
		_playerCamera.gameObject.transform.position = new Vector3(pos.x + 4, pos.y + 5, pos.z - 15);
		_mainCamera.gameObject.SetActive(false);
		_mainCamera.GetComponent<AudioListener>().enabled = false;
		_playerCamera.gameObject.SetActive(true);
		_playerCamera.GetComponent<AudioListener>().enabled = true;
		_playerCanvas.gameObject.SetActive(true);
		isFocused = true;
		_roulette.StartSpin();
	}
	public void UnfocusOnPlayer( EPlayer player )
	{
		_mainCamera.gameObject.SetActive(true);
		_mainCamera.GetComponent<AudioListener>().enabled = true;
		_playerCamera.gameObject.SetActive(false);
		_playerCamera.GetComponent<AudioListener>().enabled = false;
		_playerCanvas.gameObject.SetActive(false);
		isFocused = false;
		isProcessFinished = false;
		isWaitForAnim = false;
		_nameText.Reset();
		_turnText.Reset();
		Utils.Instance.GetPlayerByColor(currentPlayer).ResumeAnim();
	}

	public bool GetIsFocused()
	{
		return isFocused;
	}

	public bool IsProcessFinished()
	{
		return isProcessFinished;
	}
	public int GetNumber()
	{
		return number;
	}

}
