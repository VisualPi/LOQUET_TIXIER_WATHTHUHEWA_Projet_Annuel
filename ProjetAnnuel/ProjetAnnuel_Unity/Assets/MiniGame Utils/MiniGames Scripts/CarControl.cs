using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class CarControl : MonoBehaviour {
    Rigidbody carRigid;
    Transform carTransform;
    public int playerID;
    public int lapsDone;
    public bool checkpointReach;
    public int position;
    public bool enableController;
    bool isGrounded;

    [SerializeField]
    AudioClip accelerationSound;

    [SerializeField]
    AudioClip slowSound;

    AudioSource audio;

    Vector3 raycastForGround;
    


	// Use this for initialization
	void Start () {

        
        
        enableController = false;
        checkpointReach = false;
        lapsDone = 0;


        carRigid = GetComponent<Rigidbody>();
        carTransform = GetComponent<Transform>();
        audio = GetComponent<AudioSource>();

        


    }

    void OnDrawGizmosSelected()
    {
     //    Gizmos.color = Color.red;
       // Vector3 direction = carTransform.TransformDirection(Vector3.down) ;
       // Gizmos.DrawRay(carTransform.position, raycastForGround);
    }

	// Update is called once per frame
	void Update () {
        if (enableController /*&& Physics.Raycast(carTransform.position, raycastForGround, 10.0F)*/)
        {
            if (playerID == 1)
            {
                //Player 1
                if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Joystick1Button5))
                {
                    
                    carRigid.AddForce(carTransform.forward * 18, ForceMode.Acceleration);
                    
                    //Play Sound
                    if (!audio.isPlaying)
                    {
                        audio.Play();
                    }

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
            else if (playerID == 2)
            {
                //Player 2        
                if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Joystick2Button6))
                {
                    Debug.Log(carTransform.position.ToString());
                    carRigid.AddForce(carTransform.forward * 18, ForceMode.Acceleration);
                    //Play SOUND 
                    if (!audio.isPlaying)
                    {
                        audio.Play();
                    }
                }
               
                if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick2Button5))
                {
                    carRigid.velocity = carRigid.velocity * 0.9f;
                }

                if (Input.GetAxis("Horizontal2") == -1 || Input.GetAxis("Horizontal2") == 1)
                {
                    carTransform.Rotate(new Vector3(0, Input.GetAxis("Horizontal2") * 2, 0));
                }
            }

            else if (playerID == 3)
            {
                //Player 3
                if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Joystick3Button5))
                {
                    carRigid.AddForce(carTransform.forward * 12, ForceMode.Acceleration);
                    //Play SOUND
                    if (!audio.isPlaying)
                    {
                        audio.Play();
                    }
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
            else if (playerID == 4)
            {
                //Player 4
                if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Joystick4Button5))
                {
                    carRigid.AddForce(carTransform.forward * 12, ForceMode.Acceleration);
                    //Play SOUND
                    if (!audio.isPlaying)
                    {
                        audio.Play();
                    }
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
            else if (playerID == 0)
            {
                //IA
                // Let the IA system play

            }

            //audio.Stop();
        }




    }
}
