using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class PatternColorGame
{
    public List<Vector3> _positions;

    public int _count;

    public int _indexBottom;
    public int _indexTop;
    public int _indexLeft;
    public int _indexRight;

    public PatternColorGame()
    {
        _positions = new List<Vector3>();

        _indexBottom = -1;
        _indexTop = -1;
        _indexLeft = -1;
        _indexRight = -1;
    }

    public PatternColorGame(List<Vector3> positions)
    {
        _positions = positions;
        _count = positions.Count;
        SetIndex();
    }

    void SetIndex()
    {
        _indexTop = 0;
        _indexBottom = 0;
        _indexLeft = 0;
        _indexRight = 0;

        for (int i = 1; i < _positions.Count; ++i)
        {
            if (_positions[i].z > _positions[_indexTop].z)
            {
                _indexTop = i;
            }

            if (_positions[i].z < _positions[_indexBottom].z)
            {
                _indexBottom = i;
            }

            if (_positions[i].x > _positions[_indexRight].x)
            {
                _indexRight = i;
            }

            if (_positions[i].x < _positions[_indexLeft].x)
            {
                _indexLeft = i;
            }
        }
    }

    public float GetPatternHeight()
    { 
        return (_positions[_indexTop].z - _positions[_indexBottom].z);
    }

    public float GetPatternWidth()
    {
        return (_positions[_indexRight].x - _positions[_indexLeft].x);
    }

    public bool GoToRight()
    {
        return false;    
    }
}
