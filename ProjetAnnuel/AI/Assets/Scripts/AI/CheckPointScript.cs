using UnityEngine;
using System.Collections;

public class CheckPointScript : MonoBehaviour 
{
    public bool _enableGizmos;

    public float _radius;

    void OnDrawGizmos()
    {
        if (_enableGizmos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}
