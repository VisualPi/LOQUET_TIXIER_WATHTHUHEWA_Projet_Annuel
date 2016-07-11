using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

public class WriteXmlRaceScript 
{
    public string _fileName = "Assets/Saves/race_game_ai";
    public string _extention = ".xml";
    RaceGameAi _raceGameAi;

  
	string FileNemForSavingSharableIA(string playerName)
    {
        return _fileName + "_" + playerName + _extention;
    }

    void LoadAiFile(string playerName)
    {
        string fileName = FileNemForSavingSharableIA(playerName);
        if (File.Exists(fileName))
        {
            XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(RaceGameAi));
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);

            _raceGameAi = (RaceGameAi)reader.Deserialize(file);

            file.Close();
        }
        else
        {
            CreateXmlFile(playerName);

            _raceGameAi = new RaceGameAi();
        }
    }

	
	// Update is called once per frame
	

    void CreateXmlFile(string playerName)
    {
        string fileName = FileNemForSavingSharableIA(playerName);

        System.Xml.Serialization.XmlSerializer writer = new XmlSerializer(typeof(RaceGameAi));
        System.IO.FileStream file = System.IO.File.Create(fileName);

        writer.Serialize(file, new RaceGameAi());

        file.Close();
    }

    public void WriteInXMLFile(PatternRaceGame actualPatternToCheck, string playerName)
    {
        string fileName = FileNemForSavingSharableIA(playerName);

        LoadAiFile(playerName);

        if (_raceGameAi != null)
        {
            if (_raceGameAi._listPatternRaceGame.Count < _raceGameAi._maxPatternCount)
            {
                _raceGameAi._listPatternRaceGame.Add(actualPatternToCheck);
            }
            else
            {
                int indexMinScore = 0;
                for (int i = 1; i < _raceGameAi._listPatternRaceGame.Count; i++)
                {
                    if (_raceGameAi._listPatternRaceGame[i]._score < _raceGameAi._listPatternRaceGame[indexMinScore]._score)
                        indexMinScore = i;
                }

                _raceGameAi._listPatternRaceGame[indexMinScore] = actualPatternToCheck;
            }


            System.Xml.Serialization.XmlSerializer writer = new XmlSerializer(typeof(RaceGameAi));
            System.IO.FileStream file = System.IO.File.Create(fileName);

            writer.Serialize(file, _raceGameAi);

            file.Close();
        }
        else
        {
            Debug.Log("Error XML");
        }
    }
}
