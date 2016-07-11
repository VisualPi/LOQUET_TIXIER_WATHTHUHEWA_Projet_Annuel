using UnityEngine;
using System.Collections;

public class RecordPlayerDatasScript : MonoBehaviour 
{
    [SerializeField]
    CarControl carControl;

    [SerializeField]
    AiScript _aiScript;


    PatternRaceGame patternRace;

    public float _timeStart;
    public float _timeEnd;
    public float _raceTime;

    public float _score;

    public float _currentAngle = 0;

    public float _angleSumInside = 0; //somme de _lookInsideStraightCount
    public float _angleSumOutside = 0; // somme de _lookOutsideStraightCount

    public float _lookInsideStraightCount = 0; //regarde dans la direction du virage
    public float _lookOutsideStraightCount = 0; //regarde dans la direction opposé du virage

    public float _insideStraight = 0;
    public float _outsideStraight = 0;

    public float _insideTurnCount = 0;
    public float _outsideTurnCount = 0;

	// Use this for initialization
	void Start () 
    {       
        //PlayerPrefs.SetString("PLAYER_BLUE_NAME", "HARRY");
        _timeStart = -1.0f;
        _timeEnd = -1.0f;
        _raceTime = -1.0f;
	}

    // Update is called once per frame
    void Update()
    {
       
        if(carControl.position != 0 && _raceTime < 0 && !_aiScript.isInTheLab)
        {
           _timeEnd = Time.time;
            _raceTime = _timeEnd - _timeStart;
            _score = 1.0f / _raceTime * 1000.0f;


            float _averageAngleInside;
            float _averageAngleOutside;
            float _percentInsideTurn;
            float _percentInsideStraightLine;
            float _minDistanceTurn = 0.0f;

            _percentInsideTurn = _insideTurnCount / (_lookInsideStraightCount + _lookOutsideStraightCount) * 100.0f;
            _percentInsideStraightLine = _insideStraight / (_lookInsideStraightCount + _lookOutsideStraightCount) * 100.0f;
            _averageAngleInside = _angleSumInside / _lookInsideStraightCount;
            _averageAngleOutside = _angleSumOutside / _lookOutsideStraightCount;


            Debug.Log("Ecriture");
            patternRace = new PatternRaceGame(_score,_percentInsideStraightLine,_percentInsideTurn, _averageAngleInside,_averageAngleOutside, _minDistanceTurn);

           WriteXmlRaceScript writeXmlRaceScript = new WriteXmlRaceScript();
            
            Debug.Log(PlayerPrefs.GetString(carControl.playerName).ToUpper());

            writeXmlRaceScript.WriteInXMLFile(patternRace,PlayerPrefs.GetString(carControl.playerName).ToUpper());

            Debug.Log("Fin écriture");
        }
        
        if (carControl.enableController && carControl.position == 0)
        {
            if(_timeStart < 0 )
            {
                _timeStart  = Time.time;
            }
            if (_aiScript.HasCheckPoints())
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
                                _angleSumInside += _currentAngle;
                            }
                            else
                            {
                                _angleSumOutside += _currentAngle;
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
                                _angleSumInside += _currentAngle;
                            }
                            else
                            {
                                _angleSumOutside += _currentAngle;
                            }
                        }
                    }

                   // Debug.Log((_angleSumInside / _lookInsideStraightCount) + " --- " + (_angleSumOutside / _lookOutsideStraightCount));
                }
            }

        }
    }
}
