using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class PlayerInput 
{
    public int _frameNumber;
    public List<KeyCode> _keyCodes;
    public Vector3 _position;
    public Quaternion _rotation;

    public PlayerInput()
    {
        _keyCodes = new List<KeyCode>();
    }

    public PlayerInput(int frameNumber, List<KeyCode> keyCodes, Vector3 position, Quaternion rotation)
    { 
        _frameNumber = frameNumber;
        _keyCodes = keyCodes;
        _position = position;
        _rotation = rotation;
    }

    public PlayerInput(List<KeyCode> keyCodes)
    {
        _keyCodes = keyCodes;
    }
}
