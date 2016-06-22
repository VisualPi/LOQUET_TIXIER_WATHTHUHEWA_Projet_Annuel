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

    public bool _playerMove;

    Vector3 _lastDirection;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
        //Input.
    }

    void FixedUpdate()
    {
        if(_playerMove)
        {
            Vector3 direction = Vector3.zero;

            float axisX = Input.GetAxis("Vertical");
            float axisY = Input.GetAxis("Horizontal");

            if (axisX != 0)
            {
                direction += (Vector3.forward * axisX);
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


            if (axisY != 0)
            {
                _playerTransform.Rotate(Vector3.up * axisY * _rotationSpeed * Time.deltaTime, Space.Self);
            }


            if (!(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
            {
                float deceleration = _deceleration * Time.deltaTime;

                if (_speed > deceleration)
                    _speed -= deceleration;
                else if (_speed < -deceleration)
                    _speed += deceleration;
                else
                    _speed = 0;
            }

            _playerTransform.Translate(_lastDirection * Time.deltaTime * _speed, Space.Self);
        }
    }

    public void makeMove(PlayerMove playerMove, PlayerInput playerInput)
    {
        if (!_playerMove)
        {
            Vector3 direction = Vector3.zero;

            float axisX = 0;
            float axisY = 0;
            
            if(playerMove != null)
            {
                axisX = playerMove._xAxis;
                axisY = playerMove._yAxis;
            }

            KeyCode keyCode = KeyCode.None;
            if (playerInput != null)
                keyCode = playerInput._inputKey;

            if (axisX != 0)
            {
                direction += (Vector3.forward * axisX);
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

            if (axisY != 0)
            {
                _playerTransform.Rotate(Vector3.up * axisY * _rotationSpeed * Time.deltaTime, Space.Self);
            }

            
            if (!((keyCode != KeyCode.UpArrow) || (keyCode != KeyCode.DownArrow)))
            {
                float deceleration = _deceleration * Time.deltaTime;

                if (_speed > deceleration)
                    _speed -= deceleration;
                else if (_speed < -deceleration)
                    _speed += deceleration;
                else
                    _speed = 0;
            }

            _playerTransform.Translate(_lastDirection * Time.deltaTime * _speed, Space.Self);
        }
    }
}
