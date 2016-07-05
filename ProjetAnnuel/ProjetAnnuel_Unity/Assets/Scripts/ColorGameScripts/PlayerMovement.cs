using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Vector4 _delimitation = new Vector4(45f, 0f, 0, -45f); //maxX, minX, maxZ, minZ
    public Vector3 nextStep;

    public void Start()
    {
        nextStep = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    void Update()
    {
        GetInputs();
    }

    public void GetInputs()
    {
        if (Input.GetAxis("Horizontal") == -1 || Input.GetAxis("Horizontal") == 1)
        {
            Debug.Log("DROITE BLEU");
            nextStep = new Vector3(transform.position.x + (Input.GetAxis("Horizontal") * 5f), transform.position.y, transform.position.z);
        }
        if (Input.GetAxis("Vertical") == -1 || Input.GetAxis("Vertical") == 1)
        {
            nextStep = new Vector3(transform.position.x, transform.position.y, transform.position.z + (Input.GetAxis("Vertical") * -5f));
        }

        if (Input.GetAxis("Horizontal2") == -1 || Input.GetAxis("Horizontal2") == 1)
        {
            Debug.Log("DROITE VERT");
            nextStep = new Vector3(transform.position.x + (Input.GetAxis("Horizontal2") * 5f), transform.position.y, transform.position.z);
        }
        if (Input.GetAxis("Vertical2") == -1 || Input.GetAxis("Vertical2") == 1)
        {
            nextStep = new Vector3(transform.position.x, transform.position.y, transform.position.z + (Input.GetAxis("Vertical2") * 5f));
        }

        if (Input.GetAxis("Horizontal3") == -1 || Input.GetAxis("Horizontal3") == 1)
        {
            nextStep = new Vector3(transform.position.x + (Input.GetAxis("Horizontal3") * 5f), transform.position.y, transform.position.z);
        }
        if (Input.GetAxis("Vertical3") == -1 || Input.GetAxis("Vertical3") == 1)
        {
            nextStep = new Vector3(transform.position.x, transform.position.y, transform.position.z + (Input.GetAxis("Vertical3") * 5f));
        }

        if (Input.GetAxis("Horizontal4") == -1 || Input.GetAxis("Horizontal4") == 1)
        {
            nextStep = new Vector3(transform.position.x + (Input.GetAxis("Horizontal4") * 5f), transform.position.y, transform.position.z);
        }
        if (Input.GetAxis("Vertical4") == -1 || Input.GetAxis("Vertical4") == 1)
        {
            nextStep = new Vector3(transform.position.x, transform.position.y, transform.position.z + (Input.GetAxis("Vertical4") * 5f));
        }

        //if (Input.GetKey(KeyCode.Z))
        //{
        //    nextStep = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5f);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    nextStep = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5f);
        //}
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    nextStep = new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    nextStep = new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z);
        //}
    }
}
