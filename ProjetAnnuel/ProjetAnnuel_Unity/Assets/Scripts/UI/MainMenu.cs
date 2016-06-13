using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Transform MainMenuPanel;
    public Transform StartGamePanel;

	private string playerBlueName = "PlayerName1";
    private string playerRedName = "PlayerName2";
    private string playerGreenName = "PlayerName3";
    private string playerYellowName = "PlayerName4";

	private bool playerBlueIsAI;
	private bool playerRedIsAI;
	private bool playerGreenIsAI;
	private bool playerYellowIsAI;


	public void OnClickExit()
    {
        Application.Quit();
    }
    public void OnClickStart()
    {
        MainMenuPanel.gameObject.SetActive(false);
        StartGamePanel.gameObject.SetActive(true);
    }
    public void OnClickReturnFromStartPanel()
    {
        StartGamePanel.gameObject.SetActive(false);
        MainMenuPanel.gameObject.SetActive(true);
    }

    public void OnBlueNameEdit(string name)
    {
        playerBlueName = name;
    }
    public void OnRedNameEdit(string name)
    {
        playerRedName = name;
    }
    public void OnGreenNameEdit(string name)
    {
        playerGreenName = name;
    }
    public void OnYellowNameEdit(string name)
    {
        playerYellowName = name;
    }
	public void OnBlueAIChecked( bool value )
	{
		playerBlueIsAI = value;
	}
	public void OnRedAIChecked( bool value )
	{
		playerRedIsAI = value;
	}
	public void OnGreenAIChecked( bool value )
	{
		playerGreenIsAI = value;
	}
	public void OnYellowAIChecked( bool value )
	{
		playerYellowIsAI = value;
	}
	public void OnValidateButtonClick()
	{
		Debug.Log("Player Blue : " + playerBlueName + " is AI = " + playerBlueIsAI);
		Debug.Log("Player Red : " + playerRedName + " is AI = " + playerRedIsAI);
		Debug.Log("Player Green : " + playerGreenName + " is AI = " + playerGreenIsAI);
		Debug.Log("Player Yellow : " + playerYellowName + " is AI = " + playerYellowIsAI);
		//assigner variables
		PlayerPrefs.SetString("playerBlueName", playerBlueName);
		PlayerPrefs.SetString("playerRedName", playerRedName);
		PlayerPrefs.SetString("playerGreenName", playerGreenName);
		PlayerPrefs.SetString("playerYellowName", playerYellowName);
		PlayerPrefs.SetInt("playerBlueIsAI", playerBlueIsAI ? 1 : 0);
		PlayerPrefs.SetInt("playerRedIsAI", playerRedIsAI ? 1 : 0);
		PlayerPrefs.SetInt("playerGreenIsAI", playerGreenIsAI ? 1 : 0);
		PlayerPrefs.SetInt("playerYellowIsAI", playerYellowIsAI ? 1 : 0);
		SceneManager.LoadScene("main");
	}

}
