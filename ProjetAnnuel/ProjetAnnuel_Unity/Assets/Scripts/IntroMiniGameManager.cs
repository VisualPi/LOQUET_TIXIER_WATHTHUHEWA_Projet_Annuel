using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IntroMiniGameManager : MonoBehaviour {


    [SerializeField]
    Text infoMiniGameText;

    [SerializeField]
    Text TitleText;

    [SerializeField]
    Text ControlsTitleText;

    [SerializeField]
    Text ControlsText;

    // Use this for initialization
    void Start () {

        if(PlayerPrefs.GetInt("GAME_MINIGAME")==0) // voiture
        {
            TitleText.text = "THE RACE";
            infoMiniGameText.text = "Try to reach the end line first in this mini game !\r No powerup ! No cheating ! Just Skillzzzzz !!";
            ControlsTitleText.text = "Controls";
            ControlsText.text = "Movement";
            StartCoroutine(LaunchMiniGame(3));
        }
        else
        {
            TitleText.text = "THE COLOR GAME";
            infoMiniGameText.text = "Try to color the are by doing square or rectangle !\r Earn point at each square or rectangle complete ! \r No powerUp ! No cheating ! Just Skillzzzzz!!";
            ControlsTitleText.text = "Controls";
            ControlsText.text = "Movement";
            StartCoroutine(LaunchMiniGame(4));
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator LaunchMiniGame(int value)
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(value);
    }
}
