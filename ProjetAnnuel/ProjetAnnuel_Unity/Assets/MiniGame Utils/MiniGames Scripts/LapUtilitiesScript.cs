using UnityEngine;
using System.Collections;

public class LapUtilitiesScript : MonoBehaviour {

    BoxCollider startLine;

    [SerializeField]
    GameObject BlueCar;

    [SerializeField]
    GameObject RedCar;

    [SerializeField]
    GameObject GreenCar;

    [SerializeField]
    GameObject YellowCar;


    CarControl BlueCarScript;
    CarControl YellowCarScript;
    CarControl RedCarScript;
    CarControl GreenCarScript;

    //TODO
    public int LapsToDo;
    int finishPosition;

    // Use this for initialization
    void Start () {
        finishPosition = 1;
        startLine = GetComponent<BoxCollider>();
        BlueCarScript = BlueCar.GetComponent<CarControl>();
        YellowCarScript = YellowCar.GetComponent<CarControl>();
        RedCarScript = RedCar.GetComponent<CarControl>();
        GreenCarScript = GreenCar.GetComponent<CarControl>();
    }
	
	// Update is called once per frame
	void Update () {
       
	
	}

    void OnTriggerEnter(Collider car)
    {

        if (car.gameObject.name == "Blue_Car")
        {
            if(BlueCarScript.checkpointReach)
            {
                BlueCarScript.lapsDone++;
                BlueCarScript.checkpointReach = false;
                Debug.Log(BlueCarScript.lapsDone.ToString());
                if (BlueCarScript.lapsDone == LapsToDo)
                {
                    BlueCarScript.position = finishPosition;
                    finishPosition++;
                    Debug.Log(BlueCarScript.position.ToString());
                }
            }
            
        }
        if (car.gameObject.name == "Red_Car") 
        {
            if(RedCarScript.checkpointReach)
            {
                RedCarScript.lapsDone++;
                RedCarScript.checkpointReach = false;
                Debug.Log(RedCarScript.lapsDone.ToString());
                if (RedCarScript.lapsDone == LapsToDo)
                {
                    RedCarScript.position = finishPosition;
                    finishPosition++;
                    Debug.Log(RedCarScript.position.ToString());
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
                    Debug.Log(GreenCarScript.position.ToString());
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
                    Debug.Log(YellowCarScript.position.ToString());
                }
            }
        }

    }
}
