using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class RaceGameAi
{
    public List<PatternRaceGame> _listPatternRaceGame;
    public int _maxPatternCount = 6;
    

    public RaceGameAi()
    {
        _listPatternRaceGame = new List<PatternRaceGame>();
    }

    public RaceGameAi(List<PatternRaceGame> listPatternRaceGame)
    {
        _listPatternRaceGame = listPatternRaceGame;

   
    }
}
