using UnityEngine;
using System.Collections;

public class CheckPointScript : MonoBehaviour 
{
    [SerializeField]
    public bool _enableGizmos;

    [SerializeField]
    public Vector3 _cubeSize;

    [SerializeField]
    public BoxCollider _boxCollider;

    void OnDrawGizmos()
    {
        if (_enableGizmos)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, _boxCollider.size);
        }
    }
}
