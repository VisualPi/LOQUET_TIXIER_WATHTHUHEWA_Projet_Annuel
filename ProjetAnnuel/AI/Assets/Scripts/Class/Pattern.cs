using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Pattern 
{
    public int _score;
    public List<PlayerInput> _playerInputs;

    public Pattern()
    {
        _score = 0;
        _playerInputs = new List<PlayerInput>();
    }

    public Pattern(int score, List<PlayerInput> playerInputs)
    {
        _score = score;
        _playerInputs = playerInputs;
    }
}
