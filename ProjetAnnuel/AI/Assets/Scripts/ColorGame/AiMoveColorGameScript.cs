using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class AiMoveColorGameScript : MonoBehaviour 
{
    [SerializeField]
    ColorSquareScript _colorSquareScript;

    [SerializeField]
    MovePlayerColorGameScript _movePlayerColorGameScript;

    List<PatternColorGame> _listPatterns;

    int _indexCurrentPattern;

    PatternColorGame _currentPattern;

    List<int> __indexCurrentPattern;

    //int _currentTargetIndex;
    Vector3 _currentTarget;

	// Use this for initialization
	void Start () 
    {
        if (!_movePlayerColorGameScript._playerMove)
        {
            InitializePatterns();

            StartCoroutine(AiMove());
        }
	}
	
	// Update is called once per frame
    void Update()
    {
    }

    IEnumerator AiMove()
    {       
        while (true)
        {
            SetCurrentPattern();

            yield return new WaitForSeconds(1);

            int j;
            for (int i = 0; i < _currentPattern._count; ++i)
            {
                //_currentTargetIndex = i;
                _currentTarget = _currentPattern._positions[i];
                j = 0;
                while (((transform.position.x != _currentTarget.x) || (transform.position.z != _currentTarget.z)) && j < 10)
                {
                    MakeAiMove();
                    ++j;
                    yield return new WaitForSeconds(_movePlayerColorGameScript._secondBetweenMove + Random.Range(0.05f, 0.2f));
                }

            }

            //_currentTargetIndex = 0;
            _currentTarget = _currentPattern._positions[0];
            j = 0;
            while (((transform.position.x != _currentTarget.x) || (transform.position.z != _currentTarget.z)) && j < 10)
            {
                MakeAiMove();
                ++j;
                yield return new WaitForSeconds(_movePlayerColorGameScript._secondBetweenMove + Random.Range(0.05f, 0.2f));
            }

            _colorSquareScript.ResetBoard();
        }
    }

    void MakeAiMove()
    {
        Vector3 distance = transform.InverseTransformVector(_currentTarget - transform.position);

        if (distance.z > 0)
        {
            _movePlayerColorGameScript.Move(KeyCode.UpArrow);
        }
        else if (distance.z < 0)
        {
            _movePlayerColorGameScript.Move(KeyCode.DownArrow);
        }
        else if (distance.x > 0)
        {
            _movePlayerColorGameScript.Move(KeyCode.RightArrow);
        }
        else if (distance.x < 0)
        {
            _movePlayerColorGameScript.Move(KeyCode.LeftArrow);
        }
    }

    void InitializePatterns()
    {
        _listPatterns = new List<PatternColorGame>();

        List<Vector3> patternSquare = new List<Vector3>();

        patternSquare.Add(new Vector3(0, 0, 0));
        patternSquare.Add(new Vector3(4, 0, 0));
        patternSquare.Add(new Vector3(4, 0, 4));
        patternSquare.Add(new Vector3(0, 0, 4));

        /*
        patternSquare.Add(new Vector3(6, 0, 0));
        patternSquare.Add(new Vector3(10, 0, 0));
        patternSquare.Add(new Vector3(10, 0, 4));
        patternSquare.Add(new Vector3(6, 0, 4));
        */

        /*
        patternSquare.Add(new Vector3(6, 0, 6));
        patternSquare.Add(new Vector3(10, 0, 6));
        patternSquare.Add(new Vector3(10, 0, 10));
        patternSquare.Add(new Vector3(6, 0, 10));
        */

        /*
        patternSquare.Add(new Vector3(0, 0, 6));
        patternSquare.Add(new Vector3(4, 0, 6));
        patternSquare.Add(new Vector3(4, 0, 10));
        patternSquare.Add(new Vector3(0, 0, 10));
        */

        _listPatterns.Add(new PatternColorGame(patternSquare));

        List<Vector3> patternBigSquare = new List<Vector3>();
        patternBigSquare.Add(new Vector3(0, 0, 0));
        patternBigSquare.Add(new Vector3(10, 0, 0));
        patternBigSquare.Add(new Vector3(10, 0, 10));
        patternBigSquare.Add(new Vector3(0, 0, 10));

        _listPatterns.Add(new PatternColorGame(patternBigSquare));

        List<Vector3> patternRandom = new List<Vector3>();
        patternRandom.Add(new Vector3(0, 0, 0));
        patternRandom.Add(new Vector3(0, 0, 4));
        patternRandom.Add(new Vector3(4, 0, 4));
        patternRandom.Add(new Vector3(4, 0, 8));
        patternRandom.Add(new Vector3(8, 0, 8));
        patternRandom.Add(new Vector3(8, 0, 0));

        _listPatterns.Add(new PatternColorGame(patternRandom));

        _currentPattern = _listPatterns[Random.Range(0, _listPatterns.Count)];
    }

    void SetCurrentPattern()
    {
        _currentPattern = new PatternColorGame();

        List<Vector3> positions = new List<Vector3>();

        _indexCurrentPattern = Random.Range(0, _listPatterns.Count);

        _currentPattern._count = _listPatterns[_indexCurrentPattern]._count;

        _currentPattern._indexBottom = _listPatterns[_indexCurrentPattern]._indexBottom;
        _currentPattern._indexTop = _listPatterns[_indexCurrentPattern]._indexTop;
        _currentPattern._indexLeft = _listPatterns[_indexCurrentPattern]._indexLeft;
        _currentPattern._indexRight = _listPatterns[_indexCurrentPattern]._indexRight;

        for (int i = 0; i < _listPatterns[_indexCurrentPattern]._count; ++i)
        {
            positions.Add(_listPatterns[_indexCurrentPattern]._positions[i]);
        }

        Vector3 previousPosition = positions[0];
        Vector3 currentPosition;
        Vector3 difference;

        positions[0] = transform.position;

        for (int i = 1; i < positions.Count; ++i)
        {
            currentPosition = positions[i];

            difference = currentPosition - previousPosition;

            positions[i] = positions[i - 1] + difference;

            previousPosition = currentPosition;
        }

        _currentPattern._positions = positions;
    }

    bool PatternPossible()
    {
        float patternHeight = _currentPattern.GetPatternHeight();
        float patternWidth = _currentPattern.GetPatternWidth();

        if(patternWidth <= _movePlayerColorGameScript._boardWidth && patternHeight <= _movePlayerColorGameScript._boardHeight)
        {
            float distanceUp = _movePlayerColorGameScript._cubeTopLeft.position.z - transform.position.z;
            float distanceDown = transform.position.z - _movePlayerColorGameScript._cubeBottomLeft.position.z;            
            float distanceRight = _movePlayerColorGameScript._cubeBottomRight.position.x - transform.position.x;
            float distanceLeft = transform.position.x - _movePlayerColorGameScript._cubeBottomLeft.position.x;

            bool canGoUp = false;
            bool canGoDown = false;
            bool canGoRight = false;
            bool canGoLeft = false;

            if ((patternWidth <= distanceUp || patternWidth <= distanceDown) && (patternHeight <= distanceRight || patternHeight <= distanceLeft))
            {
                if(patternWidth <= distanceLeft)
                {
                    canGoLeft = true;
                }
                else if(patternWidth <= distanceRight)
                {
                    canGoRight = true;
                }

                if (patternHeight <= distanceUp)
                {
                    canGoUp = true;
                }
                else if (patternHeight <= distanceDown)
                {
                    canGoDown = true;
                }

                MakePatternPossible(canGoUp, canGoDown, canGoRight, canGoLeft);
            }
            else
            {
                // le décalage le plus petit pour atteindre entre la position et le top/bottom/left/right
            }
        }
        return false;
    }

    void MakePatternPossible(bool canGoUp, bool canGoDown, bool canGoRight, bool canGoLeft)
    {
        if(canGoUp)
        {
            if(canGoRight)
            {

            }
            else
            {

            }
        }
        else
        {
            if (canGoRight)
            {

            }
            else
            {

            }
        }
    }

    void FillPositionsList(bool goUp, bool goRight)
    {
        int indexBegin;

        if (goUp)
        {
            indexBegin = _listPatterns[_indexCurrentPattern]._indexBottom;
        }
        else
        {
            indexBegin = _listPatterns[_indexCurrentPattern]._indexTop;
        }

        if(goRight)
        {

        }
        else
        {

        }
    }


}
