using UnityEngine;
using System.Collections;

public class PlayerInput 
{
    public int _frameNumber;
    public KeyCode _inputKey;
    public Vector3 _position;
    public Quaternion _rotation;

    public PlayerInput()
    {
    }

    public PlayerInput(int frameNumber, KeyCode inputKey, Vector3 position, Quaternion rotation)
    { 
        _frameNumber = frameNumber;
        _inputKey = inputKey;
        _position = position;
        _rotation = rotation;
    }

    public override string ToString()
    {
        return "Input : " + _inputKey + " " + _frameNumber;
    }
}
