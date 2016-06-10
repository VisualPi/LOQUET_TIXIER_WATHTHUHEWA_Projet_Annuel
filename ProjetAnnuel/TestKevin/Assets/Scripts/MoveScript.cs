using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour 
{
    [SerializeField]
    Transform _playerTransform;

    [SerializeField]
    float _speed;

    [SerializeField]
    float _maxSpeed;

    [SerializeField]
    float _rotationSpeed;

    [SerializeField]
    float _acceleration;

    [SerializeField]
    float _deceleration;

    Vector3 _lastDirection; 

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            direction += Vector3.back;
        }


        if (direction != Vector3.zero)
        {
            direction = direction.normalized;

            if (direction != _lastDirection)
            {
                _lastDirection = direction;
                _speed /= 3;
            }

            if (_speed < _maxSpeed)
                _speed += (_acceleration * Time.deltaTime);
            else
                _speed = _maxSpeed;
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _playerTransform.Rotate(Vector3.down * _rotationSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _playerTransform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime, Space.Self);
        }


        if (!(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            float deceleration = _deceleration * Time.deltaTime;

            if(_speed > deceleration)
                _speed -= deceleration;
            else if(_speed < -deceleration)
                _speed += deceleration;
            else
                _speed = 0;
        }

        _playerTransform.Translate(_lastDirection * Time.deltaTime * _speed, Space.Self);
	}
}
