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

    List<PlayerInput> _playerInputs;
    int _nbInputsToDo;
    int _currentInputToDo;
    PlayerInput _currentPlayerInput;

    //int _tickStart;
    
    bool _actionsLaunched;

	// Use this for initialization
	void Start () 
    {
        Initialization();
	}
	
    void Initialization()
    {
        _playerInputs = new List<PlayerInput>();
        _nbInputsToDo = 0;
        _currentInputToDo = 0;
        _currentPlayerInput = null;

        //_tickStart = 0;

        _actionsLaunched = false;
    }

	// Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.P)) && (_actionsLaunched == false))
        {
            Debug.Log("Make Action");
            
            MakePlayerActions();
        }

        if (_nbInputsToDo > 0)
        {            
            _currentPlayerInput = _playerInputs[_currentInputToDo];

            ++_currentInputToDo;
            --_nbInputsToDo;

            if (_currentPlayerInput != null)
                _moveScript.makeMove(_currentPlayerInput);

            if (_nbInputsToDo <= 0)
            {
                _currentInputToDo = 0;

                _actionsLaunched = false;

                _moveScript._playerMove = true;

                Debug.Log("----- End -----");
            }
        }
	}

    void MakePlayerActions()
    {
        XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(RaceGameAi));
        System.IO.StreamReader file = new System.IO.StreamReader(FILE_PATH);
        
        _raceGameAi = (RaceGameAi)reader.Deserialize(file);

        file.Close();

        _playerInputs = _raceGameAi._patternVirage[0]._playerInputs;

        _nbInputsToDo = _playerInputs.Count;


        if (_nbInputsToDo > 0)
        {
            _currentInputToDo = 0;

            _actionsLaunched = true;
            
            _moveScript._playerMove = false;
            
            //_tickStart = Time.frameCount;

            Debug.Log("----- Begin -----");
        }
    }
}
