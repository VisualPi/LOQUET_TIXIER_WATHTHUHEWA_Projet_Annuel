using UnityEngine;
using System.Collections;

public class CheckPointScript : MonoBehaviour {

    BoxCollider startLine;

    [SerializeField]
    GameObject BlueCar;

    [SerializeField]
    GameObject RedCar;

    [SerializeField]
    GameObject GreenCar;

    [SerializeField]
    GameObject YellowCar;


    [SerializeField]
    CheckPointScript LastCkeckPoint;


    [SerializeField]
    int checkpointID;

    [SerializeField]
    BoxCollider colliderBox;

    public Vector3 _cubeSize;

    CarControl BlueCarScript;
    CarControl YellowCarScript;
    CarControl RedCarScript;
    CarControl GreenCarScript;

    
    public bool yellowCheckpassed;
    bool redCheckpassed;
    bool greenCheckpassed;
    bool blueCheckpassed;


    // Use this for initialization
    void Start()
    {
        if(checkpointID == 23)
        {
            yellowCheckpassed = true;
            redCheckpassed = true;
            greenCheckpassed = true;
            blueCheckpassed = true;

        }
        else
        {
            yellowCheckpassed = false;
            redCheckpassed = false;
            greenCheckpassed = false;
            blueCheckpassed = false;
        }
        
        startLine = GetComponent<BoxCollider>();
        BlueCarScript = BlueCar.GetComponent<CarControl>();
        YellowCarScript = YellowCar.GetComponent<CarControl>();
        RedCarScript = RedCar.GetComponent<CarControl>();
        GreenCarScript = GreenCar.GetComponent<CarControl>();

        _cubeSize = colliderBox.size;
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter(Collider car)
    {

        if (car.gameObject.name == "Blue_car")
        {
            if (LastCkeckPoint.blueCheckpassed == true)
            {

                blueCheckpassed = true;
                LastCkeckPoint.LastCkeckPoint.blueCheckpassed = false;
                BlueCarScript.checkpointsPassed += 1;
                

                 Debug.Log("CHECKPOINT " + checkpointID);
                Debug.Log(BlueCarScript.checkpointsPassed);
            }


        }
        if (car.gameObject.name == "Red_car")
        {

            if (LastCkeckPoint.redCheckpassed == true)
            {

                redCheckpassed = true;
                LastCkeckPoint.LastCkeckPoint.redCheckpassed = false;
                RedCarScript.checkpointsPassed += 1;
               
            }
        }
        if (car.gameObject.name == "Green_car")
        {

            if (LastCkeckPoint.greenCheckpassed == true)
            {

                greenCheckpassed = true;
                LastCkeckPoint.LastCkeckPoint.greenCheckpassed = false;
                GreenCarScript.checkpointsPassed += 1;
              
            }
        }
        if (car.gameObject.name == "Yellow_car")
        {
            if (LastCkeckPoint.yellowCheckpassed == true)
            {

                yellowCheckpassed = true;
                LastCkeckPoint.LastCkeckPoint.yellowCheckpassed = false;
                YellowCarScript.checkpointsPassed += 1;
               
              
             
            }
           
            
            
        }

    }
}
