using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

public class WriteXmlScript : MonoBehaviour 
{
    const string FILE_PATH = "race_game_ai.xml";

    RaceGameAi _raceGameAi;

    List<PlayerMove> _playerMoves;
    float _previousXAxis;
    float _previousYAxis;

    List<PlayerInput> _playerInputs;

    int _tickStart;

    bool _recording;

	// Use this for initialization
	void Start () 
    {
        LoadAiFile();

        Initialization();
	}

    void LoadAiFile()
    {
        if (File.Exists(FILE_PATH))
        {
            XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(RaceGameAi));
            System.IO.StreamReader file = new System.IO.StreamReader(FILE_PATH);

            _raceGameAi = (RaceGameAi)reader.Deserialize(file);

            file.Close();
        }
        else
        {
            CreateXmlFile();

            _raceGameAi = new RaceGameAi();
        }
    }

    void Initialization()
    {
        _recording = true;

        _tickStart = Time.frameCount;

        _playerMoves = new List<PlayerMove>();
        _previousXAxis = 0;
        _previousYAxis = 0;

        _playerInputs = new List<PlayerInput>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void FixedUpdate()
    {
        DetectMove();
        DetectKey();

        if (Input.GetKey(KeyCode.Space))
        {
            if (_recording == true)
            {
                _recording = false;
                Debug.Log("Write - Begin");
                WriteInXMLFile();
                Debug.Log("Write - End");
            }
        }
    }

    void DetectMove()
    {
        float xAxis = Input.GetAxis("Vertical");
        float yAxis = Input.GetAxis("Horizontal");

        if ((xAxis != _previousXAxis) || (yAxis != _previousYAxis))
        {
            _playerMoves.Add(new PlayerMove(xAxis, yAxis, transform.position, transform.rotation, (Time.frameCount - _tickStart)));

            _previousXAxis = xAxis;
            _previousYAxis = yAxis;
        }
    }

    void DetectKey()
    {
        DetectKeyDown();
        DetectKeyUp();
    }

    void DetectKeyDown()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            _playerInputs.Add(new PlayerInput((Time.frameCount - _tickStart), KeyCode.UpArrow, transform.position, transform.rotation));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _playerInputs.Add(new PlayerInput((Time.frameCount - _tickStart), KeyCode.DownArrow, transform.position, transform.rotation));
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _playerInputs.Add(new PlayerInput((Time.frameCount - _tickStart), KeyCode.LeftArrow, transform.position, transform.rotation));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _playerInputs.Add(new PlayerInput((Time.frameCount - _tickStart), KeyCode.RightArrow, transform.position, transform.rotation));
        }
    }

    void DetectKeyUp()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            _playerInputs.Add(new PlayerInput((Time.frameCount - _tickStart), KeyCode.UpArrow, transform.position, transform.rotation));
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            _playerInputs.Add(new PlayerInput((Time.frameCount - _tickStart), KeyCode.DownArrow, transform.position, transform.rotation));
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _playerInputs.Add(new PlayerInput((Time.frameCount - _tickStart), KeyCode.LeftArrow, transform.position, transform.rotation));
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _playerInputs.Add(new PlayerInput((Time.frameCount - _tickStart), KeyCode.RightArrow, transform.position, transform.rotation));
        }
    }

    void CreateXmlFile()
    {
        System.Xml.Serialization.XmlSerializer writer = new XmlSerializer(typeof(RaceGameAi));
        System.IO.FileStream file = System.IO.File.Create(FILE_PATH);

        writer.Serialize(file, new RaceGameAi());

        file.Close();
    }

    void WriteInXMLFile()
    {
        _raceGameAi._patternVirage[0]._playerMoves = _playerMoves;
        _raceGameAi._patternVirage[0]._playerInputs = _playerInputs;

        System.Xml.Serialization.XmlSerializer writer = new XmlSerializer(typeof(RaceGameAi));
        System.IO.FileStream file = System.IO.File.Create(FILE_PATH);

        writer.Serialize(file, _raceGameAi);

        file.Close();
    }
}
