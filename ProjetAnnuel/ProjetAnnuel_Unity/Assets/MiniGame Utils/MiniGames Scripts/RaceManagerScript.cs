using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
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


    AudioSource audio;
        

    CarControl BlueCarScript;
    CarControl YellowCarScript;
    CarControl RedCarScript;
    CarControl GreenCarScript;

    
    public int LapsToDo;
    int finishPosition;
    bool isGameEnd;

    //TODO
    //HASHTABLE POUR AFFICHER LES VAINQUEURS


	// Use this for initialization
	void Start () {

        SetDisplayInfo();

        startCountdownText.gameObject.SetActive(false);
        
        finishPosition = 1;
        isGameEnd = false;
        BlueCarScript = BlueCar.GetComponent<CarControl>();
        YellowCarScript = YellowCar.GetComponent<CarControl>();
        RedCarScript = RedCar.GetComponent<CarControl>();
        GreenCarScript = GreenCar.GetComponent<CarControl>();

        audio = GetComponent<AudioSource>();
        
        //TO DO : INCLURE LA MUSIQUE DE DEBUT et faire demarrer la coroutine à la fin de celle çi
        //audio.PlayOneShot(startRaceSong);

       
       


        //Movement of camera for starting



        

        //Countdown for start
        StartCoroutine(StartCountdown());
        
	
	}
	
	// Update is called once per frame
	void Update () {

        // FINISH POSITION DOIT ETRE EGAL A 5 POUR LE CHANGEMENT DE SCENE
        if (finishPosition == 2 && !isGameEnd)
        {
            
            isGameEnd = true;
            StartCoroutine(ChangingScene());

        }
        
	}

    
    void OnTriggerEnter(Collider car)
    {
        
        if (car.gameObject.name == "Blue_Car")
        {
            if (BlueCarScript.checkpointReach)
            {
                BlueCarScript.lapsDone++;
                BlueCarScript.checkpointReach = false;
                Debug.Log(BlueCarScript.lapsDone.ToString());
                if (BlueCarScript.lapsDone == LapsToDo)
                {
                    BlueCarScript.position = finishPosition;
                    finishPosition++;
                    blueText.text = BlueCarScript.position.ToString();
                    blueText.gameObject.SetActive(true);
                    BlueCarScript.enableController = false;
                    
                }
            }

        }
        if (car.gameObject.name == "Red_Car")
        {
            if (RedCarScript.checkpointReach)
            {
                RedCarScript.lapsDone++;
                RedCarScript.checkpointReach = false;
                Debug.Log(RedCarScript.lapsDone.ToString());
                if (RedCarScript.lapsDone == LapsToDo)
                {
                    RedCarScript.position = finishPosition;
                    finishPosition++;
                    redText.text = RedCarScript.position.ToString();
                    redText.gameObject.SetActive(true);
                    RedCarScript.enableController = false;
                }
            }

        }
        if (car.gameObject.name == "Green_Car")
        {
            if (GreenCarScript.checkpointReach)
            {
                GreenCarScript.lapsDone++;
                GreenCarScript.checkpointReach = false;
                Debug.Log(GreenCarScript.lapsDone.ToString());
                if (GreenCarScript.lapsDone == LapsToDo)
                {
                    GreenCarScript.position = finishPosition;
                    finishPosition++;
                    greenText.text = GreenCarScript.position.ToString();
                    greenText.gameObject.SetActive(true);
                    GreenCarScript.enableController = false;
                }
            }
        }
        if (car.gameObject.name == "Yellow_Car")
        {
            if (YellowCarScript.checkpointReach)
            {
                YellowCarScript.lapsDone++;
                YellowCarScript.checkpointReach = false;
                Debug.Log(YellowCarScript.lapsDone.ToString());
                if (YellowCarScript.lapsDone == LapsToDo)
                {
                    YellowCarScript.position = finishPosition;
                    finishPosition++;
                    Debug.Log("END");
                    yellowText.text = YellowCarScript.position.ToString();
                    yellowText.gameObject.SetActive(true);
                    YellowCarScript.enableController = false;
                }
            }
        }

    }


    void SetDisplayInfo()
    {
        blueText.gameObject.SetActive(false);
        redText.gameObject.SetActive(false);
        greenText.gameObject.SetActive(false);
        yellowText.gameObject.SetActive(false);

    }

    IEnumerator StartCountdown()
    {

        startCountdownText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        startCountdownText.text = "READY !";
        //PLAY SOUND
        yield return new WaitForSeconds(0.5f);
        audio.PlayOneShot(audioCountdown);
        yield return new WaitForSeconds(0.5f);
        startCountdownText.text = "3";
        //PLAY SOUND
        
        

        yield return new WaitForSeconds(1);
        startCountdownText.text = "2";
        //PLAY SOUND

        yield return new WaitForSeconds(1);
        startCountdownText.text = "1";
        //PLAY SOUND
        yield return new WaitForSeconds(1);
        startCountdownText.text = "GO !";
        //PLAY SOUND


        BlueCarScript.enableController = true;
        RedCarScript.enableController = true;
        YellowCarScript.enableController = true;
        GreenCarScript.enableController = true;

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
