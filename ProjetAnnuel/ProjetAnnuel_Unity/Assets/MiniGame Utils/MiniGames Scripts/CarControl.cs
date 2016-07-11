using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(AudioSource))]
public class CarControl : MonoBehaviour {
    [SerializeField]
    Rigidbody carRigid;
    [SerializeField]
    Transform carTransform;

    Quaternion rotato;
    public int playerID;
    public int lapsDone;
   // public bool checkpointReach;
    public int checkpointsPassed;
    public int position;
    public bool enableController;
    bool isGrounded;

    [SerializeField]
    float _maxVelocity;

    [SerializeField]
    AudioClip accelerationSound;

    [SerializeField]
    AudioClip slowSound;

    [SerializeField]
    AudioClip idleSound;

    [SerializeField]
    AudioSource audio;

    Vector3 raycastForGround;

    [SerializeField]
    public KeyCode _inputForwardCar;

    [SerializeField]
    public KeyCode _inputBrakeCar;

    [SerializeField]
    public KeyCode _inputResetCar;

    [SerializeField]
    string axisNameCar;

    [SerializeField]
    PathScript Path;

    [SerializeField]
    AiScript aiScript;

    public bool isAI;

    [SerializeField]
    string playerPrefName;

    [SerializeField]
    MeshRenderer Body;

    [SerializeField]
    MeshRenderer Wheel;

    [SerializeField]
    MeshRenderer Wheel2;

    [SerializeField]
    MeshRenderer Wheel3;

    [SerializeField]
    MeshRenderer Wheel4;

   bool oneStop;

    // Use this for initialization
    void Start () {

        if (PlayerPrefs.GetInt(playerPrefName) == 1)
        {
            isAI = true;
        }

        oneStop = false;
        enableController = false;
        //checkpointReach = false;
        lapsDone = 0;
        checkpointsPassed = 0;
        carRigid = GetComponent<Rigidbody>();
        carTransform = GetComponent<Transform>();
       // audio = GetComponent<AudioSource>();
        rotato = carTransform.rotation;
        //audio.
        //audio.Play();
        audio.loop = true;

    }

    void OnDrawGizmosSelected()
    {
     //    Gizmos.color = Color.red;
       // Vector3 direction = carTransform.TransformDirection(Vector3.down) ;
       // Gizmos.DrawRay(carTransform.position, raycastForGround);
    }

    void Update()
    {
        aiScript.GetNextPath();
        if(enableController == true)
        {
            Debug.Log("GOGO");
        }
        if(enableController && !oneStop)
        {
            Debug.Log("ETEINT");
            audio.loop = false;
            audio.Stop();
            oneStop = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (enableController)
        {
                if (Input.GetKey(_inputForwardCar))
                {

                    carRigid.AddForce(carTransform.forward * 18 * 1.5f, ForceMode.Acceleration);

                    //Play Sound
                    if (!audio.isPlaying)
                    {
                        audio.Play();
                        audio.loop = true;
                    }
                    
                }
                if (Input.GetKey(_inputBrakeCar))
                {
                carRigid.AddForce(carTransform.forward * -18, ForceMode.Acceleration);
                    //carRigid.velocity = carRigid.velocity * 0.9f;
                }

                if (Input.GetAxis(axisNameCar) >= -1 || Input.GetAxis(axisNameCar) <= 1)
                {
                    carTransform.Rotate(new Vector3(0, Input.GetAxis(axisNameCar) * 2, 0));
                }

                if (Input.GetKey(_inputResetCar))
                {
                    Debug.Log("ResetCar");
                // carTransform = Path.yellowTransform;
                carRigid.velocity = new Vector3(0,0,0);
                carTransform.position = new Vector3(aiScript._previousCheckPoint.transform.position.x,0.3f, aiScript._previousCheckPoint.transform.position.z);
                carTransform.LookAt(aiScript._currentCheckPoint.transform);
                StartCoroutine(ResetCar());
                }
           
        }

    }


        public void MoveCarWithIA(PlayerInput playerInput)
        {   
            if (playerInput._keyCodes.Contains(_inputForwardCar))
            {
                carRigid.AddForce(carTransform.forward * 18 * 1.5f, ForceMode.Acceleration);

            if(carRigid.velocity.magnitude > _maxVelocity)
            {
                carRigid.velocity = carRigid.velocity.normalized * _maxVelocity;
            }

                //Play Sound
                if (!audio.isPlaying)
                {
                    audio.Play();
                }

            }
            if (playerInput._keyCodes.Contains(_inputBrakeCar))
            {
                carRigid.velocity = carRigid.velocity * 0.9f;
            }

            if (playerInput._axeHorizontal <= 1 && playerInput._axeHorizontal >= -1) // peut etre zone morte a ajouter entre 0.1 et -0.1
            {
                carTransform.Rotate(new Vector3(0, playerInput._axeHorizontal * 2, 0));
            }

            if (playerInput._keyCodes.Contains(_inputResetCar))
            {
                Debug.Log("ResetCar");
                carTransform.rotation = rotato;
            }
        }

        IEnumerator ResetCar()
        {
            enableController = false;
            yield return new WaitForSeconds(0.1f);
            Body.enabled = false;
            yield return new WaitForSeconds(0.1f);
            Body.enabled = true;
            yield return new WaitForSeconds(0.1f);
            Body.enabled = false;
            yield return new WaitForSeconds(0.1f);
            Body.enabled = true;
            enableController = true;
            yield return new WaitForSeconds(0.1f);
            Body.enabled = false;
            yield return new WaitForSeconds(0.1f);
            Body.enabled = true;
        
        }
    }
