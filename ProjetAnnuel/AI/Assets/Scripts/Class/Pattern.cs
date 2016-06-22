using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Pattern 
{
    public int _score;
    public List<PlayerMove> _playerMoves;
    public List<PlayerInput> _playerInputs;

    public Pattern()
    {
        _score = 0;
        _playerMoves = new List<PlayerMove>();
        _playerInputs = new List<PlayerInput>();
    }

    public Pattern(int score, List<PlayerMove> playerMoves, List<PlayerInput> playerInputs)
    {
        _score = score;
        _playerMoves = playerMoves;
        _playerInputs = playerInputs;
    }
}
