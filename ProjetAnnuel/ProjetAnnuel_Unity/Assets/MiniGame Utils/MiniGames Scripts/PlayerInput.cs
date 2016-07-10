using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class PlayerInput 
{
    public int _frameNumber;
    public List<KeyCode> _keyCodes;
    public Vector3 _position;
    public Quaternion _rotation;

    public float _axeHorizontal;

    public PlayerInput()
    {
        _keyCodes = new List<KeyCode>();
    }

    public PlayerInput(int frameNumber, List<KeyCode> keyCodes, Vector3 position, Quaternion rotation, float axeX)
    { 
        _frameNumber = frameNumber;
        _keyCodes = keyCodes;
        _position = position;
        _rotation = rotation;
        _axeHorizontal = axeX;
    }

    public PlayerInput(List<KeyCode> keyCodes, float axeX)
    {
        _keyCodes = keyCodes;
        _axeHorizontal = axeX;
    }
}
