using UnityEngine;
using System.Collections;

public class CarControl : MonoBehaviour {
    Rigidbody carRigid;
    Transform carTransform;
    public int playerID;
    public int lapsDone;
    public bool checkpointReach;
    public int position;

	// Use this for initialization
	void Start () {

        
        carRigid = GetComponent<Rigidbody>();
        carTransform = GetComponent<Transform>();
        lapsDone = 0;
        checkpointReach = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (playerID == 1)
        {
            //Player 1
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Joystick1Button5))
            {
                carRigid.AddForce(carTransform.forward * 12, ForceMode.Acceleration);
            }
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button6))
            {
                carRigid.velocity = carRigid.velocity * 0.9f;
            }

            if (Input.GetAxis("Horizontal") == -1 || Input.GetAxis("Horizontal") == 1)
            {
                carTransform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * 2, 0));
            }
        }
        if (playerID == 2)
        {
            //Player 2        
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Joystick2Button5))
            {
                carRigid.AddForce(carTransform.forward * 12, ForceMode.Acceleration);
            }
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick2Button6))
            {
                carRigid.velocity = carRigid.velocity * 0.9f;
            }

            if (Input.GetAxis("Horizontal2") == -1 || Input.GetAxis("Horizontal2") == 1)
            {
                carTransform.Rotate(new Vector3(0, Input.GetAxis("Horizontal2") * 2, 0));
            }
        }

        if (playerID == 3)
        {
            //Player 3
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Joystick3Button5))
            {
                carRigid.AddForce(carTransform.forward * 12, ForceMode.Acceleration);
            }
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick3Button6))
            {
                carRigid.velocity = carRigid.velocity * 0.9f;
            }

            if (Input.GetAxis("Horizontal3") == -1 || Input.GetAxis("Horizontal3") == 1)
            {
                carTransform.Rotate(new Vector3(0, Input.GetAxis("Horizontal3") * 2, 0));
            }
        }
        if (playerID == 4)
        {
            //Player 4
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Joystick4Button5))
            {
                carRigid.AddForce(carTransform.forward * 12, ForceMode.Acceleration);
            }
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick4Button6))
            {
                carRigid.velocity = carRigid.velocity * 0.9f;
            }

            if (Input.GetAxis("Horizontal4") == -1 || Input.GetAxis("Horizontal4") == 1)
            {
                carTransform.Rotate(new Vector3(0, Input.GetAxis("Horizontal4") * 2, 0));
            }
        }





    }
}
