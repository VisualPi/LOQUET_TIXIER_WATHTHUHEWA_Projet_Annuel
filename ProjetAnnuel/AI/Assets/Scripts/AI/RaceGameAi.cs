using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class RaceGameAi
{
    public List<Pattern> _patternVirage;
    public List<Pattern> _patternLigneDroite;

    public RaceGameAi()
    {
    }

    public RaceGameAi(List<Pattern> patternVirage, List<Pattern> patternLigneDroite)
    {
        _patternVirage = patternVirage;
        _patternLigneDroite = patternLigneDroite;
    }
}
