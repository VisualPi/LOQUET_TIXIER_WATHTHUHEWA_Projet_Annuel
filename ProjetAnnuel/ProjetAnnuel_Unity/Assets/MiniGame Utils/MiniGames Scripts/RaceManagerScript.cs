using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(AudioSource))]
public class RaceManagerScript : MonoBehaviour {

    [SerializeField]
    Text startCountdownText;

    [SerializeField]
    Text blueText;

    [SerializeField]
    Text yellowText;

    [SerializeField]
    Text redText;

    [SerializeField]
    Text greenText;

    [SerializeField]
    Text blueLapsText;

    [SerializeField]
    Text yellowLapsText;

    [SerializeField]
    Text redLapsText;

    [SerializeField]
    Text greenLapsText;

    [SerializeField]
    GameObject BlueCar;

    [SerializeField]
    GameObject RedCar;

    [SerializeField]
    GameObject GreenCar;

    [SerializeField]
    GameObject YellowCar;

    [SerializeField]
    AudioClip startRaceSong;

    [SerializeField]
    AudioClip audioCountdown;

    [SerializeField]
    AudioClip audioChangingScene;

    [SerializeField]
    AudioClip raceMusic;

    [SerializeField]
    PathScript path;

    [SerializeField]
    AudioSource audio;
        
    [SerializeField]
    CarControl BlueCarScript;
    [SerializeField]
    CarControl YellowCarScript;
    [SerializeField]
    CarControl RedCarScript;
    [SerializeField]
    CarControl GreenCarScript;

    public int LapsToDo;
    int finishPosition;
    bool isGameEnd;

    //TODO
    //HASHTABLE POUR AFFICHER LES VAINQUEURS

	// Use this for initialization
	IEnumerator Start () {
        //InitForPosition and game
        finishPosition = 1;
        isGameEnd = false;

       

        SetDisplayInfo();

        //Movement of camera for starting
        

        //Countdown for start
        yield return StartCoroutine(IntroRoutine());
        
	
	}
	
	// Update is called once per frame
	void Update () {

        // FINISH POSITION DOIT ETRE EGAL A 5 POUR LE CHANGEMENT DE SCENE
        // finishPosition == 2 est la valeur de test (c'est a dire que des qu'une voiture passe la ligne la partie se finit) mettre 5 pour la version release
        if (finishPosition == 5 && !isGameEnd)
        {
            
            isGameEnd = true;
            StartCoroutine(ChangingScene());

        }
        
	}

    //Gestion des tours (Nombre de checkpoints codé en dur actuellement)
    void OnTriggerEnter(Collider car)
    {
        if (car.gameObject.name == "Blue_car")
        {
            if (BlueCarScript.checkpointsPassed >= path._path.Count - 3)
            {
                BlueCarScript.lapsDone++;
                BlueCarScript.checkpointsPassed = 0;
                blueLapsText.text = (BlueCarScript.lapsDone + 1) + "/" + LapsToDo.ToString() + " Laps";

                if (BlueCarScript.lapsDone == LapsToDo)
                {
                    blueLapsText.gameObject.SetActive(false);
                    BlueCarScript.position = finishPosition;
					PlayerPrefs.SetInt("PLAYER_BLUE_WIN", finishPosition);
					finishPosition++;
                    blueText.text = BlueCarScript.position.ToString();
                    blueText.gameObject.SetActive(true);
                    BlueCarScript.isAI = true;
                    //BlueCarScript.enableController = false;
                }
            }
            else
            {
                BlueCarScript.checkpointsPassed = 0;
            }
        }
        if (car.gameObject.name == "Red_car")
        {
            if (RedCarScript.checkpointsPassed >= path._path.Count - 3)
            {
                RedCarScript.lapsDone++;
                RedCarScript.checkpointsPassed = 0;
                redLapsText.text = (RedCarScript.lapsDone + 1) + "/" + LapsToDo.ToString() + " Laps";
              
                if (RedCarScript.lapsDone == LapsToDo)
                {
                    redLapsText.gameObject.SetActive(false);
                    RedCarScript.position = finishPosition;
					PlayerPrefs.SetInt("PLAYER_RED_WIN", finishPosition);
					finishPosition++;
                    redText.text = RedCarScript.position.ToString();
                    redText.gameObject.SetActive(true);
                    // RedCarScript.enableController = false;
                    RedCarScript.isAI = true;
                }
                else
                {
                    BlueCarScript.checkpointsPassed = 0;
                }
            }
        }
        if (car.gameObject.name == "Green_car")
        {
            if (GreenCarScript.checkpointsPassed >= path._path.Count - 3)
            {
                GreenCarScript.lapsDone++;
                GreenCarScript.checkpointsPassed = 0;
                greenLapsText.text = (GreenCarScript.lapsDone + 1) + "/" + LapsToDo.ToString() + " Laps";
                if (GreenCarScript.lapsDone == LapsToDo)
                {
                    greenLapsText.gameObject.SetActive(false);
                    GreenCarScript.position = finishPosition;
					PlayerPrefs.SetInt("PLAYER_GREEN_WIN", finishPosition);
					finishPosition++;
                    greenText.text = GreenCarScript.position.ToString();
                    greenText.gameObject.SetActive(true);
                    // GreenCarScript.enableController = false;
                    GreenCarScript.isAI = true;
                }
                else
                {
                    BlueCarScript.checkpointsPassed = 0;
                }
            }
        }
        if (car.gameObject.name == "Yellow_car")
        {
            if (YellowCarScript.checkpointsPassed >= path._path.Count - 3)
            {
                YellowCarScript.lapsDone++;
                YellowCarScript.checkpointsPassed = 0;
                yellowLapsText.text = (YellowCarScript.lapsDone + 1) + "/" + LapsToDo.ToString() + " Laps";
               // Debug.Log(YellowCarScript.lapsDone.ToString());
                if (YellowCarScript.lapsDone == LapsToDo)
                {
                    yellowLapsText.gameObject.SetActive(false);
                    YellowCarScript.position = finishPosition;
					PlayerPrefs.SetInt("PLAYER_YELLOW_WIN", finishPosition);
					finishPosition++;
                    yellowText.text = YellowCarScript.position.ToString();
                    yellowText.gameObject.SetActive(true);
                    //  YellowCarScript.enableController = false;
                    YellowCarScript.isAI= true;
                }
                else
                {
                    BlueCarScript.checkpointsPassed = 0;
                }
            }
        }

    }


    void SetDisplayInfo()
    {
        startCountdownText.gameObject.SetActive(false);
        blueText.gameObject.SetActive(false);
        redText.gameObject.SetActive(false);
        greenText.gameObject.SetActive(false);
        yellowText.gameObject.SetActive(false);

        blueLapsText.text = (BlueCarScript.lapsDone+1) + "/" + LapsToDo.ToString() + " Laps";
        redLapsText.text = (RedCarScript.lapsDone+1) + "/" + LapsToDo.ToString() + " Laps";
        greenLapsText.text = (GreenCarScript.lapsDone+1) + "/" + LapsToDo.ToString() + " Laps";
        yellowLapsText.text = (YellowCarScript.lapsDone+1) + "/" + LapsToDo.ToString() + " Laps";

      

    }

    IEnumerator IntroRoutine()
    {
        audio.PlayOneShot(startRaceSong);
        yield return new WaitForSeconds(startRaceSong.length-1);

        startCountdownText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        startCountdownText.text = "READY !";
        
        yield return new WaitForSeconds(0.5f);
        audio.PlayOneShot(audioCountdown);
        yield return new WaitForSeconds(0.5f);
        startCountdownText.text = "3";
        
        yield return new WaitForSeconds(1);
        startCountdownText.text = "2";
       
        yield return new WaitForSeconds(1);
        startCountdownText.text = "1";
        
        yield return new WaitForSeconds(1);
        startCountdownText.text = "GO !";
        
        BlueCarScript.enableController = true;
        RedCarScript.enableController = true;
        YellowCarScript.enableController = true;
        GreenCarScript.enableController = true;
        audio.PlayOneShot(raceMusic);

        yield return new WaitForSeconds(1);

        startCountdownText.gameObject.SetActive(false);



    }

   

    IEnumerator ChangingScene()
    {

        audio.Stop();
        audio.PlayOneShot(audioChangingScene);
        
        yield return new WaitForSeconds(audioChangingScene.length);

        SceneManager.LoadScene("Result");

    }

}
