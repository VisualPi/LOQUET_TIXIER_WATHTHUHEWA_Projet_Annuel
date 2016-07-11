using UnityEngine;
using System.Collections;

public class MovePlayerColorGameScript : MonoBehaviour 
{
    [SerializeField]
    public Transform _cubeBottomLeft;

    [SerializeField]
    public Transform _cubeBottomRight;

    [SerializeField]
    public Transform _cubeTopLeft;

    [SerializeField]
    public Transform _cubeTopRight;

    [SerializeField]
    public Transform _cubeBottomLeftLeft;

    [SerializeField]
    public Transform _cubeBottomLeftUp;

    [SerializeField]
    public float _secondBetweenMove;

    [SerializeField]
    public bool _playerMove;

    public float _distanceBetweenCubeX;
    public float _distanceBetweenCubeZ;

    public float _boardWidth;
    public float _boardHeight;


    void Start()
    {
        if(_playerMove)
            StartCoroutine(WaitInputMove());

        _distanceBetweenCubeX = _cubeBottomLeftLeft.position.x - _cubeBottomLeft.position.x;
        _distanceBetweenCubeZ = _cubeBottomLeftUp.position.z - _cubeBottomLeft.position.z;

        _boardWidth = _cubeBottomRight.position.x - _cubeBottomLeft.position.x;
        _boardHeight = _cubeTopLeft.position.z - _cubeBottomLeft.position.z;
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
                    yield return new WaitForSeconds(_secondBetweenMove);
                }
            }

            yield return new WaitForSeconds(0);
        }
    }

    public bool Move(KeyCode keyCode)
    {
        if (keyCode == KeyCode.UpArrow)
        {
            if ((transform.position + (Vector3.forward * _distanceBetweenCubeZ)).z <= _cubeTopLeft.position.z)
            {
                transform.position = transform.position + (Vector3.forward * _distanceBetweenCubeZ);
                return true;
            }
        }

        else if (keyCode == KeyCode.DownArrow)
        {
            if ((transform.position + (Vector3.back * _distanceBetweenCubeZ)).z >= _cubeBottomLeft.position.z)
            {
                transform.position = transform.position + (Vector3.back * _distanceBetweenCubeZ);
                return true;
            }
        }

        else if (keyCode == KeyCode.LeftArrow)
        {
            if ((transform.position + (Vector3.left * _distanceBetweenCubeX)).x >= _cubeTopLeft.position.x)
            {
                transform.position = transform.position + (Vector3.left * _distanceBetweenCubeX);
                return true;
            }
        }

        else if (keyCode == KeyCode.RightArrow)
        {
            if ((transform.position + (Vector3.right * _distanceBetweenCubeX)).x <= _cubeTopRight.position.x)
            {
                transform.position = transform.position + (Vector3.right * _distanceBetweenCubeX);
                return true;
            }
        }

        return false;
    }
}
