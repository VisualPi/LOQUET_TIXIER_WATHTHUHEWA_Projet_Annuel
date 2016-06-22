using UnityEngine;
using System.Collections;

public class PlayerMove 
{
    public float _frameNumber;
    public float _xAxis;
    public float _yAxis;
    public Vector3 _position;
    public Quaternion _rotation;

	public PlayerMove()
    {

    }

    public PlayerMove(float xAxix, float yAxis, Vector3 position, Quaternion rotation, int frameNumber)
    {
        _frameNumber = frameNumber;
        _xAxis = xAxix;
        _yAxis = yAxis;
        _position = position;
        _rotation = rotation;
    }

    public override string ToString()
    {
        return "Move : " + _xAxis + " ; " + _yAxis + " - " + _frameNumber;
    }
}
