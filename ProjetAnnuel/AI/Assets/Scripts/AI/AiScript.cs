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

    [SerializeField]
    float _angleMaxRange;

    [SerializeField]
    float _secondsBetweenRandom;

    float _angleApproximation;

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
                Gizmos.DrawWireSphere(_currentCheckPoint.transform.position, _currentCheckPoint._radius - 1.00f);
            }
            
            Gizmos.color = Color.yellow;

            Gizmos.DrawRay(transform.position, (transform.forward) * 3);
            /*
            Gizmos.DrawRay(transform.position, (transform.forward + -transform.right) * 5);
            Gizmos.DrawRay(transform.position, (transform.forward + transform.right) * 5);

            Gizmos.DrawRay(transform.position, (-transform.forward)*2);
            Gizmos.DrawRay(transform.position, (-transform.forward + -transform.right) * 2);
            Gizmos.DrawRay(transform.position, (-transform.forward + transform.right) * 2);

            Gizmos.DrawRay(transform.position, (transform.right)*3);
            Gizmos.DrawRay(transform.position, (-transform.right)*3);
            */
        }
    }

	// Use this for initialization
	void Start () 
    {
        _moveScript._playerMove = false;

        _path = _pathScript._path;

        if(_path.Count > 0)
            _currentCheckPoint = _path[0];

        StartCoroutine(randomAngle());
	}
	
	// Update is called once per frame
	void Update () 
    {
        Debug.Log("Update");
        Move();
	}

    IEnumerator randomAngle()
    {
        while (true)
        {
            _angleApproximation = Random.Range(0, _angleMaxRange);

            Debug.Log("random - " + _angleApproximation);

            yield return new WaitForSeconds(1);
        }
    }

    void Move()
    {
        if (_currentCheckPoint != null)
        {
            List<KeyCode> iaKeyCodes = new List<KeyCode>();
            
            float angle = Vector3.Angle(transform.forward, _currentCheckPoint.transform.position - transform.position);
           
            //if ((angle >= 0 && angle <= 45) || (angle >= 135 && angle <= 180))
            if ((angle >= 0 && angle <= _angleApproximation) || (angle >= (180-_angleApproximation) && angle <= 180))
            {
                iaKeyCodes.Add(KeyCode.UpArrow);
            }
            else
            {
                iaKeyCodes.Add(KeyCode.UpArrow);

                float angleLeft = Vector3.Angle(transform.forward - transform.right, _currentCheckPoint.transform.position - transform.position);
                float angleRight = Vector3.Angle(transform.forward + transform.right, _currentCheckPoint.transform.position - transform.position);

                if (angleLeft > angleRight)
                {
                    iaKeyCodes.Add(KeyCode.RightArrow);
                }
                else
                {
                    iaKeyCodes.Add(KeyCode.LeftArrow);
                }
            }

            PlayerInput pi = new PlayerInput(iaKeyCodes);
            _moveScript.makeMove(pi);
            /*                  
            RaycastHit rayCastHit;
                
            if (Physics.Raycast(transform.position, transform.forward, out rayCastHit, 3, 1 << LayerMask.NameToLayer("CheckPoint")))
            {
                Debug.Log("hit " + rayCastHit.collider.gameObject.name + " - " + rayCastHit.distance);
            }
            else
            {

            }
            */
        }

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
