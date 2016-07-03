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

        _playerInputs = new List<PlayerInput>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        DetectKey();

        if (Input.GetKeyDown(KeyCode.A))
            Debug.Log("A");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_recording == true)
            {
                Debug.Log("Write - Begin");
                WriteInXMLFile();
                Debug.Log("Write - End");
                _recording = false;
            }
        }
    }

    void DetectKey()
    {
        if (Input.anyKey)
        {
            List<KeyCode> keyCodes = new List<KeyCode>();

            if (Input.GetKey(KeyCode.UpArrow))
                keyCodes.Add(KeyCode.UpArrow);

            if (Input.GetKey(KeyCode.DownArrow))
                keyCodes.Add(KeyCode.DownArrow);

            if (Input.GetKey(KeyCode.RightArrow))
                keyCodes.Add(KeyCode.RightArrow);

            if (Input.GetKey(KeyCode.LeftArrow))
                keyCodes.Add(KeyCode.LeftArrow);

            _playerInputs.Add(new PlayerInput((Time.frameCount - _tickStart), keyCodes, transform.position, transform.rotation));
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
        _raceGameAi._patternVirage[0]._playerInputs = _playerInputs;

        System.Xml.Serialization.XmlSerializer writer = new XmlSerializer(typeof(RaceGameAi));
        System.IO.FileStream file = System.IO.File.Create(FILE_PATH);

        writer.Serialize(file, _raceGameAi);

        file.Close();
    }
}
