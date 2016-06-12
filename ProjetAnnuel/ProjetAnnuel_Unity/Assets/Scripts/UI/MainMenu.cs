using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public Transform MainMenuPanel;
    public Transform StartGamePanel;

    public string playerBlueName;
    private string playerRedName;
    private string playerGreenName;
    private string playerYellowName;


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

}
