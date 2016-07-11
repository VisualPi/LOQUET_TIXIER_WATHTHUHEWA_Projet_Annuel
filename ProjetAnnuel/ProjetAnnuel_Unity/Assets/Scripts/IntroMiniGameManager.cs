using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IntroMiniGameManager : MonoBehaviour {


    [SerializeField]
    Text infoMiniGameText;

	// Use this for initialization
	void Start () {

        if(PlayerPrefs.GetInt("GAME_MINIGAME")==0) // voiture
        {
            infoMiniGameText.text = "JEU DE LA VOITURE ECRIS TA VIE ICI ";
            StartCoroutine(LaunchMiniGame(3));
        }
        else
        {
            infoMiniGameText.text = "JEU DES COULEURS ECRIS TA VIE ICI";
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
