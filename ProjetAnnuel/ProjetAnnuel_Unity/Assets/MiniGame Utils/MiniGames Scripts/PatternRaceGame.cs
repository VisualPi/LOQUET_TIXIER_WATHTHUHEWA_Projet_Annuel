using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class PatternRaceGame 
{
    public float _score;
    public float _percentInsideTurn;
    public float _percentInsideStraightLine;
    public float _averageAngleInside;
    public float _averageAngleOutside;
    public float _minDistanceBeforeTurning;

    public PatternRaceGame()
    {
        _score = 0.0f;
        _percentInsideTurn = 0.0f;
        _averageAngleInside=0.0f;
        _averageAngleOutside=0.0f;
        _percentInsideStraightLine=0.0f;

    }

    public PatternRaceGame(float score,float percentInsideStraightLine, float percentInsideTurn, float averageAngleInside, float averageAngleOutside, float minDistanceBeforeTurning)
    {
        _score = score;
        _percentInsideStraightLine = percentInsideStraightLine;
        _percentInsideTurn = percentInsideTurn;
        _averageAngleInside = averageAngleInside;
        _averageAngleOutside = averageAngleOutside;
        _minDistanceBeforeTurning = minDistanceBeforeTurning;
        
    }
}
