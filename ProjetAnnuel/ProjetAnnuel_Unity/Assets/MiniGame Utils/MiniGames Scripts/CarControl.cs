using UnityEngine;
using System.Collections;

public class CarControl : MonoBehaviour {
    Rigidbody carRigid;
    Transform carTransform;
	// Use this for initialization
	void Start () {
        carRigid = GetComponent<Rigidbody>();
        carTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetKey(KeyCode.Z))
        {
            carRigid.AddForce(carTransform.forward * 15,ForceMode.Acceleration);
        }
        
        if(Input.GetKey(KeyCode.D))
        {
            carTransform.Rotate(new Vector3(0, 5, 0));
        }
        
        if(Input.GetKey(KeyCode.Q))
        {
            carTransform.Rotate(new Vector3(0, -5, 0));
            
        }
        
        if(Input.GetKey(KeyCode.S))
        {
            carRigid.AddForce(-carTransform.forward * 10   , ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            carRigid.velocity = carRigid.velocity * 0.7f;
        }
	}
}
