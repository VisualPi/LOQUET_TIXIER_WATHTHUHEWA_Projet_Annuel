using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Xml.Serialization;

public class MakeActionsScript : MonoBehaviour 
{
    [SerializeField]
    MoveScript _moveScript;

    const string FILE_PATH = "race_game_ai.xml";

    RaceGameAi _raceGameAi;

    List<PlayerMove> _playerMoves;
    int _nbMovesToDo;
    int _currentMoveToDo;
    PlayerMove _currentPlayerMove;

    List<PlayerInput> _playerInputs;
    int _nbInputsToDo;
    int _currentInputToDo;
    PlayerInput _currentPlayerInput;
    PlayerInput _previousPlayerInput;

    int _tickStart;
    
    bool _actionsLaunched;

	// Use this for initialization
	void Start () 
    {
        Initialization();
	}
	
    void Initialization()
    {
        _playerMoves = new List<PlayerMove>(); ;
        _nbMovesToDo = 0;
        _currentMoveToDo = 0;
        _currentPlayerMove = null;

        _playerInputs = new List<PlayerInput>();
        _nbInputsToDo = 0;
        _currentInputToDo = 0;
        _currentPlayerInput = null;
        _previousPlayerInput = null;

        _tickStart = 0;

        _actionsLaunched = false;
    }

	// Update is called once per frame
    void Update()
    {
        //_moveScript.makeMove(_currentKey);
    }
	void FixedUpdate() 
    {
        if ((Input.GetKey(KeyCode.P)) && (_actionsLaunched == false))
        {
            Debug.Log("Make Action");
            
            MakePlayerActions();
        }

        int currentTick = Time.frameCount - _tickStart;

        if (_nbMovesToDo > 0)
        {
            if (currentTick >= _playerMoves[_currentMoveToDo]._frameNumber)
            {
                _currentPlayerMove = _playerMoves[_currentMoveToDo];

                ++_currentMoveToDo;
                --_nbMovesToDo;
            }
        }

        if (_nbInputsToDo > 0)
        {
            if (currentTick >= _playerInputs[_currentInputToDo]._frameNumber)
            {
                _previousPlayerInput = _currentPlayerInput;
                _currentPlayerInput = _playerInputs[_currentInputToDo];

                ++_currentInputToDo;
                --_nbInputsToDo;
            }
        }

        if (_currentPlayerMove != null || _currentPlayerInput != null)
        {
            if (currentTick >= _currentPlayerMove._frameNumber || currentTick >= _currentPlayerInput._frameNumber)
            {
                _moveScript.makeMove(_currentPlayerMove, _currentPlayerInput);
            }
            
            if(_previousPlayerInput != null && _currentPlayerInput != null)
            {
                if(_previousPlayerInput._inputKey == _currentPlayerInput._inputKey)
                {
                    _previousPlayerInput = null;
                    _currentPlayerInput = null;
                }
            }
            
            if (_nbMovesToDo == 0 && _nbInputsToDo == 0)
            {
                if (currentTick >= _currentPlayerMove._frameNumber)
                {
                    Debug.Log("----- End -----");

                    Initialization();

                    _moveScript._playerMove = true;
                }
            }
        }
	}

    void MakePlayerActions()
    {
        XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(RaceGameAi));
        System.IO.StreamReader file = new System.IO.StreamReader(FILE_PATH);
        
        _raceGameAi = (RaceGameAi)reader.Deserialize(file);

        file.Close();

        _playerMoves = _raceGameAi._patternVirage[0]._playerMoves;
        _playerInputs = _raceGameAi._patternVirage[0]._playerInputs;

        _nbMovesToDo = _playerMoves.Count;
        _nbInputsToDo = _playerInputs.Count;

        if (_nbMovesToDo > 0)
        {
            _currentMoveToDo = 0;
        }

        if(_nbInputsToDo > 0)
        {
            _currentMoveToDo = 0;
        }

        if (_nbMovesToDo > 0 || _nbInputsToDo > 0)
        {
            _actionsLaunched = true;
            _moveScript._playerMove = false;
            _tickStart = Time.frameCount;

            Debug.Log("----- Begin -----");
        }
    }
}
