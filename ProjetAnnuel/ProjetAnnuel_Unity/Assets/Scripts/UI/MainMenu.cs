using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public Transform MainMenuPanel;
	public Transform StartGamePanel;
    public Transform TheLabPanel;

	private string playerBlueName = "No name1";
	private string playerGreenName = "No name2";
	private string playerRedName = "No name3";
	private string playerYellowName = "No name4";
    private string playerLabName = "Hector";

	private bool playerBlueIsAI;
	private bool playerRedIsAI;
	private bool playerGreenIsAI;
	private bool playerYellowIsAI;



	public void OnClickExit()
	{
        PlayerPrefs.DeleteAll();
		Application.Quit();
	}
	public void OnClickStart()
	{
		MainMenuPanel.gameObject.SetActive(false);
		StartGamePanel.gameObject.SetActive(true);
	}

    public void OnClickThelab()
    {
        MainMenuPanel.gameObject.SetActive(false);
        TheLabPanel.gameObject.SetActive(true);
    }

    public void OnClickGoThelab()
    {
        PlayerPrefs.SetString("PLAYER_LAB_NAME", playerLabName);
        PlayerPrefs.SetInt("IS_IN_LAB_MODE", 1);
        SceneManager.LoadScene("TheLab");
    }
    public void OnClickReturnFromStartPanel()
	{
		StartGamePanel.gameObject.SetActive(false);
		MainMenuPanel.gameObject.SetActive(true);
	}

	public void OnBlueNameEdit( string name )
	{
		playerBlueName = name;
	}
	public void OnRedNameEdit( string name )
	{
		playerRedName = name;
	}
	public void OnGreenNameEdit( string name )
	{
		playerGreenName = name;
	}
	public void OnYellowNameEdit( string name )
	{
		playerYellowName = name;
	}
    public void OnLabnameEdit(string name)
    {
        playerLabName = name;
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
		PlayerPrefs.SetString("PLAYER_BLUE_NAME", playerBlueName);
		PlayerPrefs.SetString("PLAYER_RED_NAME", playerRedName);
		PlayerPrefs.SetString("PLAYER_GREEN_NAME", playerGreenName);
		PlayerPrefs.SetString("PLAYER_YELLOW_NAME", playerYellowName);
		PlayerPrefs.SetInt("PLAYER_BLUE_ISAI", playerBlueIsAI ? 1 : 0);
		PlayerPrefs.SetInt("PLAYER_RED_ISAI", playerRedIsAI ? 1 : 0);
		PlayerPrefs.SetInt("PLAYER_GREEN_ISAI", playerGreenIsAI ? 1 : 0);
		PlayerPrefs.SetInt("PLAYER_YELLOW_ISAI", playerYellowIsAI ? 1 : 0);
		
		
		PlayerPrefs.SetInt("PLAYER_BLUE_STARS", 0);
		PlayerPrefs.SetInt("PLAYER_GREEN_STARS", 0);
		PlayerPrefs.SetInt("PLAYER_RED_STARS", 0);
		PlayerPrefs.SetInt("PLAYER_YELLOW_STARS", 0);
		
		PlayerPrefs.SetInt("PLAYER_BLUE_CASEID", 0);
		PlayerPrefs.SetInt("PLAYER_GREEN_CASEID", 0);
		PlayerPrefs.SetInt("PLAYER_RED_CASEID", 0);
		PlayerPrefs.SetInt("PLAYER_YELLOW_CASEID", 0);

		PlayerPrefs.SetInt("PLAYER_BLUE_NB_VICTORIES", 0);
		PlayerPrefs.SetInt("PLAYER_GREEN_NB_VICTORIES", 0);
		PlayerPrefs.SetInt("PLAYER_RED_NB_VICTORIES", 0);
		PlayerPrefs.SetInt("PLAYER_YELLOW_NB_VICTORIES", 0);

		PlayerPrefs.SetInt("GAME_ISFIRSTRUN", 1);

		SceneManager.LoadScene("main");
	}

}
