using UnityEngine;
using System.Collections;

public class RecordPlayerDatasScript : MonoBehaviour 
{
    [SerializeField]
    AiScript _aiScript;

    public float _currentAngle = 0;

    public float _angleMaxInside = 0;
    public float _angleMaxOutside = 0;

    public int _lookInsideStraightCount = 0;
    public int _lookOutsideStraightCount = 0;

    public int _insideStraight = 0;
    public int _outsideStraight = 0;

    public int _insideTurnCount = 0;
    public int _outsideTurnCount = 0;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(_aiScript.HasCheckPoints())
        {
            // Pour savoir s'il regarde dans le sens du virage ou dans le sens opposé
            float angleCarForwardPreviousCurrentCheckPoint = 0;
            bool lookTurnDirection = true;

            if (_aiScript._nextCheckPoint)
            {
                angleCarForwardPreviousCurrentCheckPoint = Vector3.Angle(transform.forward, _aiScript._currentCheckPoint.transform.position - _aiScript._previousCheckPoint.transform.position);

                float angleCarForwardCurrentNextCheckPoint = Vector3.Angle(transform.forward, _aiScript._nextCheckPoint.transform.position - _aiScript._currentCheckPoint.transform.position);

                if (angleCarForwardCurrentNextCheckPoint >= 90 && angleCarForwardCurrentNextCheckPoint <= 180)
                {
                    //angleCarForwardPreviousCurrentCheckPoint = -angleCarForwardPreviousCurrentCheckPoint;
                    lookTurnDirection = false;

                    ++_lookOutsideStraightCount;
                }
                else
                    ++_lookInsideStraightCount;

                _currentAngle = angleCarForwardPreviousCurrentCheckPoint;



                // Pour savoir si le joueur est à l'interieur ou à l'exterieur du chemin
                Vector3 previous = _aiScript._previousCheckPoint.transform.position;
                Vector3 current = _aiScript._currentCheckPoint.transform.position;
                Vector3 next = _aiScript._nextCheckPoint.transform.position;
                Vector3 position = transform.position;

                float previousCurrent = ((previous.x - position.x) * (current.z - position.z)) - ((previous.z - position.z) * (current.x - position.x));
                float currentNext = ((current.x - position.x) * (next.z - position.z)) - ((current.z - position.z) * (next.x - position.x));
                float nextPrevious = ((next.x - position.x) * (previous.z - position.z)) - ((next.z - position.z) * (previous.x - position.x));

                Vector3 positionToPreviousCheckPoint = (_aiScript._previousCheckPoint.transform.position - _aiScript.transform.position);


                // Cas voiture dans l'interieur du chemin
                if ((previousCurrent > 0 && currentNext > 0 && nextPrevious > 0) || (previousCurrent < 0 && currentNext < 0 && nextPrevious < 0))
                {
                    // Virage interieur
                    if (positionToPreviousCheckPoint.magnitude <= (Mathf.Max(_aiScript._previousCheckPoint._cubeSize.x, _aiScript._previousCheckPoint._cubeSize.z) / 1.5f))
                    {
                        ++_insideTurnCount;
                    }
                    // Ligne droite interieur
                    else
                    {
                        ++_insideStraight;

                        if (lookTurnDirection)
                        {
                            _angleMaxInside += _currentAngle;
                        }
                        else
                        {
                            _angleMaxOutside += _currentAngle;
                        }
                    }
                }
                // Cas voiture dans l'exterieur du chemin
                else
                {
                    // Virage exterieur
                    if (positionToPreviousCheckPoint.magnitude <= (Mathf.Max(_aiScript._previousCheckPoint._cubeSize.x, _aiScript._previousCheckPoint._cubeSize.z) / 1.5f))
                    {
                        ++_outsideTurnCount;
                    }
                    // Ligne droite exterieur
                    else
                    {
                        ++_outsideStraight;

                        if (lookTurnDirection)
                        {
                                _angleMaxInside += _currentAngle;
                        }
                        else
                        {
                                _angleMaxOutside += _currentAngle;
                        }
                    }
                }

                Debug.Log((_angleMaxInside / _lookInsideStraightCount) + " --- " + (_angleMaxOutside / _lookOutsideStraightCount));
            }
        }

	}
}
