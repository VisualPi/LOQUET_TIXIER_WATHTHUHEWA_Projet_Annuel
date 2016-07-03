using UnityEngine;
using System.Collections;

public class PlayerCollisionScript : MonoBehaviour 
{
    [SerializeField]
    MoveScript _moveScript;

    [SerializeField]
    float _slowingCoefficient;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Cube"))
        {
            if (_moveScript.Speed > 0)
                _moveScript.Speed *= _slowingCoefficient;
        }
    }
    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Collider"))
        {
            if (other.gameObject.tag == "StartEnd")
                Debug.Log("Enter");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Collider"))
        {
            if(other.gameObject.tag == "StartEnd")
                Debug.Log("Exit");
        }
    }
    */
}
