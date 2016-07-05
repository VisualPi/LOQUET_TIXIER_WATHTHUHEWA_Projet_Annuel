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

    [SerializeField]
    bool _sharpCurve;

    float _angleApproximation;

    List<CheckPointScript> _path;

    int _currentCheckPointIndex;
    CheckPointScript _currentCheckPoint;
    CheckPointScript _previousCheckPoint;
    CheckPointScript _nextCheckPoint;

    void OnDrawGizmos()
    {
        if (_enableGizmos)
        {
            if (_currentCheckPoint != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(_currentCheckPoint.transform.position, _currentCheckPoint._radius + 2.00f);

                Gizmos.color = Color.black;
                Gizmos.DrawRay(transform.position, (_currentCheckPoint.transform.position - transform.position));
                Gizmos.DrawRay(transform.position, transform.forward * 5);
            }

            if(_previousCheckPoint != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(_previousCheckPoint.transform.position, _currentCheckPoint._radius + 2.00f);
            }

            if(_nextCheckPoint != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(_nextCheckPoint.transform.position, _currentCheckPoint._radius + 2.00f);
            }
            
            //Gizmos.color = Color.yellow;

            //Gizmos.DrawRay(transform.position, (transform.forward) * 3);
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

        if (_path.Count >= 3)
        {
            _previousCheckPoint = null;
            _currentCheckPoint = _path[0];
            _nextCheckPoint = _path[1];
        }

        StartCoroutine(randomAngle());
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log("Update");
        Move();
	}

    IEnumerator randomAngle()
    {
        while (true)
        {
            _angleApproximation = Random.Range(0, _angleMaxRange);

            yield return new WaitForSeconds(1);
        }
    }

    void Move()
    {
        if (_currentCheckPoint != null)
        {
            List<KeyCode> iaKeyCodes = new List<KeyCode>();
            
            RaycastHit rayCastHit;

            if (Physics.Raycast(transform.position, transform.forward, out rayCastHit, 10, 1 << LayerMask.NameToLayer("Cube")))
            {
                //Debug.Log("hit ");// + rayCastHit.collider.gameObject.name + " - " + rayCastHit.distance);
            }

            float angle = Vector3.Angle((_currentCheckPoint.transform.position - transform.position), transform.forward);

            if (_previousCheckPoint)
            {
                /*
                Vector3 cross = Vector3.Cross((_previousCheckPoint.transform.position - transform.position), (_currentCheckPoint.transform.position - transform.position));
                
                if (cross.y < 0)
                    Debug.Log("Interieur");
                else
                    Debug.Log("Exterieur");
                */

                /*
                Vector3 currentPrevious = (_previousCheckPoint.transform.position - _currentCheckPoint.transform.position);
                Vector3 currentNext = (_nextCheckPoint.transform.position - _currentCheckPoint.transform.position);
                */
            }

            // Si on est a peu près dans la direction du checkpoint à atteindre
            if (angle >= 0 && angle <= _angleApproximation)
            {
                // On avance tout droit
                iaKeyCodes.Add(KeyCode.UpArrow);

                // Vérification si on est à l'intèrieur ou exterieur 
                Vector3 previous = _previousCheckPoint.transform.position;
                Vector3 current = _currentCheckPoint.transform.position;
                Vector3 next = _nextCheckPoint.transform.position;
                Vector3 position = transform.position;

                float previousCurrent = ((previous.x - position.x) * (current.z - position.z)) - ((previous.z - position.z) * (current.x - position.x));
                float currentNext = ((current.x - position.x) * (next.z - position.z)) - ((current.z - position.z) * (next.x - position.x));
                float nextPrevious = ((next.x - position.x) * (previous.z - position.z)) - ((next.z - position.z) * (previous.x - position.x));

                // Cas interieur des checksPoints
                if ((previousCurrent > 0 && currentNext > 0 && nextPrevious > 0) || (previousCurrent < 0 && currentNext < 0 && nextPrevious < 0))
                {
                    float angleLeft = Vector3.Angle((_nextCheckPoint.transform.position - transform.position), -transform.right);
                    float angleRight = Vector3.Angle((_nextCheckPoint.transform.position - transform.position), transform.right);

                    // Virage interieur
                    if (_sharpCurve)
                    {
                        if (angleLeft < angleRight)
                            iaKeyCodes.Add(KeyCode.LeftArrow);
                        else
                            iaKeyCodes.Add(KeyCode.RightArrow);
                    }
                    // Virage exterieur
                    else
                    {
                        if (angleLeft < angleRight)
                            iaKeyCodes.Add(KeyCode.RightArrow);
                        else
                            iaKeyCodes.Add(KeyCode.LeftArrow);
                    }
                }
                // Cas exterieur des CheckPoints
                else
                {                 
                    float angleLeft = Vector3.Angle((_nextCheckPoint.transform.position - transform.position), -transform.right);
                    float angleRight = Vector3.Angle((_nextCheckPoint.transform.position - transform.position), transform.right);

                    // Virage interieur
                    if (_sharpCurve)
                    {
                        if (angleLeft < angleRight)
                            iaKeyCodes.Add(KeyCode.LeftArrow);
                        else
                            iaKeyCodes.Add(KeyCode.RightArrow);
                    }
                    // Virage exterieur
                    else
                    {
                        if (angleLeft < angleRight)
                            iaKeyCodes.Add(KeyCode.RightArrow);
                        else
                            iaKeyCodes.Add(KeyCode.LeftArrow);
                    }
                    
                }
            }
            // Cas voiture pas dans la direction du checkPoint à atteindre 
            else 
            {
                float angleLeft = Vector3.Angle((_currentCheckPoint.transform.position - transform.position), - transform.right);
                float angleRight = Vector3.Angle((_currentCheckPoint.transform.position - transform.position), transform.right);

                // Vérification si on est pas à l'envers
                if(!(angle >= (180 - _angleApproximation) && angle <= 180))
                {
                    // On continue à avancer mais on tourne de facon a s'aligner vers le checkPoint à atteindre
                    iaKeyCodes.Add(KeyCode.UpArrow);

                    if (angleLeft < angleRight)
                    {
                        iaKeyCodes.Add(KeyCode.LeftArrow);
                    }
                    else
                    {
                        iaKeyCodes.Add(KeyCode.RightArrow);
                    }
                }
                else
                {
                    // Marche arrière dans le cas de la marche arrière
                    iaKeyCodes.Add(KeyCode.DownArrow);

                    if (angleLeft < angleRight)
                    {
                        iaKeyCodes.Add(KeyCode.RightArrow);
                    }
                    else
                    {
                        iaKeyCodes.Add(KeyCode.LeftArrow);
                    }
                }
            }
            /*
            RaycastHit rayCastHit;

            if (Physics.Raycast(transform.position, transform.forward, out rayCastHit, 5, 1 << LayerMask.NameToLayer("Cube")))
            {
                Debug.Log("hit ");// + rayCastHit.collider.gameObject.name + " - " + rayCastHit.distance);
            }
            */
            PlayerInput pi = new PlayerInput(iaKeyCodes);
            _moveScript.makeMove(pi);

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

            _previousCheckPoint = _currentCheckPoint;
            _currentCheckPoint = _path[_currentCheckPointIndex];

            if (_currentCheckPointIndex != (_path.Count - 1))
            {
                _nextCheckPoint = _path[_currentCheckPointIndex + 1];
            }
            else
            {
                _nextCheckPoint = _path[0];
            }
        }
    }
}
