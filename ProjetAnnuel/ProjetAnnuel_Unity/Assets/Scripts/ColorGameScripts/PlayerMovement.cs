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
        if (Input.GetKey(KeyCode.Z))
        {
            nextStep = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            nextStep = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5f);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            nextStep = new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            nextStep = new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z);
        }
    }
}
