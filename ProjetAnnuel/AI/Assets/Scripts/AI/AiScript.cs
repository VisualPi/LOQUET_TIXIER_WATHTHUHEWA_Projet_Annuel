using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class AiScript : MonoBehaviour 
{
    [SerializeField]
    bool _enableGizmos;

    [SerializeField]
    MoveScript _moveScript;

    [SerializeField]
    PathScript _pathScript;

    [SerializeField]
    float _minDistanceFromCheckPoint;

    List<CheckPointScript> _path;

    int _currentCheckPointIndex;
    CheckPointScript _currentCheckPoint;


    void OnDrawGizmos()
    {
        if (_enableGizmos)
        {
            if (_currentCheckPoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(_currentCheckPoint.transform.position, _currentCheckPoint._radius + 0.10f);
            }
            
            Gizmos.color = Color.yellow;

            Gizmos.DrawRay(transform.position, (transform.forward) * 5);
            Gizmos.DrawRay(transform.position, (transform.forward + -transform.right) * 5);
            Gizmos.DrawRay(transform.position, (transform.forward + transform.right) * 5);

            Gizmos.DrawRay(transform.position, (-transform.forward)*2);
            Gizmos.DrawRay(transform.position, (-transform.forward + -transform.right) * 2);
            Gizmos.DrawRay(transform.position, (-transform.forward + transform.right) * 2);

            Gizmos.DrawRay(transform.position, (transform.right)*3);
            Gizmos.DrawRay(transform.position, (-transform.right)*3);
            
        }
    }

	// Use this for initialization
	void Start () 
    {
        _path = _pathScript._path;

        if(_path.Count > 0)
            _currentCheckPoint = _path[0];
	}
	
	// Update is called once per frame
	void Update () 
    {
        Move();
	}

    void Move()
    {
        GetNextPath();
    }

    void GetNextPath()
    {
        Vector3 positionToCurrentCheckPoint = (_currentCheckPoint.transform.position - transform.position);
        
        if(positionToCurrentCheckPoint.magnitude <= (_currentCheckPoint._radius))
        {
            if(_currentCheckPointIndex < (_path.Count-1))
            {
                ++_currentCheckPointIndex;
            }
            else
            {
                _currentCheckPointIndex = 0;
            }

            _currentCheckPoint = _path[_currentCheckPointIndex];
        }
    }
}
