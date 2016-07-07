using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class AiMoveColorGameScript : MonoBehaviour 
{
    [SerializeField]
    Transform _cubeBottomLeft;

    [SerializeField]
    Transform _cubeBottomRight;

    [SerializeField]
    Transform _cubeTopLeft;

    [SerializeField]
    Transform _cubeTopRight;

    [SerializeField]
    bool _playerMove;

    [SerializeField]
    float _secondBetweenMove;

    //bool _playerCanMove;

    List<List<Vector3>> _listPatterns;

    List<Vector3> _currentPattern;

    int _currentTargetIndex;
    Vector3 _currentTarget;

	// Use this for initialization
	void Start () 
    {
        //_playerCanMove = true;

        InitializePatterns();

        StartCoroutine(WaitInputMove());
	}
	
	// Update is called once per frame
    void Update()
    {
    }

    IEnumerator WaitInputMove()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                bool hasMoved = false;

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    hasMoved = Move(KeyCode.UpArrow);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    hasMoved = Move(KeyCode.DownArrow);
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    hasMoved = Move(KeyCode.LeftArrow);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    hasMoved = Move(KeyCode.RightArrow);
                }

                if (hasMoved)
                {
                    //_playerCanMove = false;
                    yield return new WaitForSeconds(_secondBetweenMove);
                    //_playerCanMove = true;
                }
            }

            yield return new WaitForSeconds(0);
        }
    }

    void InitializePatterns()
    {
        _listPatterns = new List<List<Vector3>>();

        List<Vector3> patternSquare = new List<Vector3>();
        patternSquare.Add(new Vector3(0, 0, 0));
        patternSquare.Add(new Vector3(4, 0, 0));
        patternSquare.Add(new Vector3(4, 0, 4));
        patternSquare.Add(new Vector3(0, 0, 4));

        _listPatterns.Add(patternSquare);

        _currentPattern = patternSquare;
    }

    void IaSetupPattern()
    {
        _currentTargetIndex = 0;
        _currentTarget = _currentPattern[_currentTargetIndex];
    }

    void Move()
    {
        if (_playerMove)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if ((transform.position + (Vector3.forward * 2)).z <= _cubeTopLeft.position.z)
                {
                    transform.position = transform.position + (Vector3.forward * 2);
                }
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if ((transform.position + (Vector3.back * 2)).z >= _cubeBottomLeft.position.z)
                {
                    transform.position = transform.position + (Vector3.back * 2);
                }
            }

            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if ((transform.position + (Vector3.left * 2)).x >= _cubeTopLeft.position.x)
                {
                    transform.position = transform.position + (Vector3.left * 2);
                }
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if ((transform.position + (Vector3.right * 2)).x <= _cubeTopRight.position.x)
                {
                    transform.position = transform.position + (Vector3.right * 2);
                }
            }
        }
    }

    bool Move(KeyCode keyCode)
    {
        if (keyCode == KeyCode.UpArrow)
        {
            if ((transform.position + (Vector3.forward * 2)).z <= _cubeTopLeft.position.z)
            {
                transform.position = transform.position + (Vector3.forward * 2);
                return true;
            }
        }

        else if (keyCode == KeyCode.DownArrow)
        {
            if ((transform.position + (Vector3.back * 2)).z >= _cubeBottomLeft.position.z)
            {
                transform.position = transform.position + (Vector3.back * 2);
                return true;
            }
        }

        else if (keyCode == KeyCode.LeftArrow)
        {
            if ((transform.position + (Vector3.left * 2)).x >= _cubeTopLeft.position.x)
            {
                transform.position = transform.position + (Vector3.left * 2);
                return true;
            }
        }

        else if (keyCode == KeyCode.RightArrow)
        {
            if ((transform.position + (Vector3.right * 2)).x <= _cubeTopRight.position.x)
            {
                transform.position = transform.position + (Vector3.right * 2);
                return true;
            }
        }

        return false;
    }
}
