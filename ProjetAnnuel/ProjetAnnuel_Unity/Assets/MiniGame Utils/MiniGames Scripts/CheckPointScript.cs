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


    CarControl BlueCarScript;
    CarControl YellowCarScript;
    CarControl RedCarScript;
    CarControl GreenCarScript;


    // Use this for initialization
    void Start()
    {
        startLine = GetComponent<BoxCollider>();
        BlueCarScript = BlueCar.GetComponent<CarControl>();
        YellowCarScript = YellowCar.GetComponent<CarControl>();
        RedCarScript = RedCar.GetComponent<CarControl>();
        GreenCarScript = GreenCar.GetComponent<CarControl>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter(Collider car)
    {

        if (car.gameObject.name == "Blue_Car")
        {
            BlueCarScript.checkpointReach = true;
               
            
        }
        if (car.gameObject.name == "Red_Car")
        {
            RedCarScript.checkpointReach = true;
           
        }
        if (car.gameObject.name == "Green_Car")
        {
            GreenCarScript.checkpointReach = true;
                
        }
        if (car.gameObject.name == "Yellow_Car")
        {
           YellowCarScript.checkpointReach = true;
           Debug.Log("CHECKPOINT");
        }

    }
}
