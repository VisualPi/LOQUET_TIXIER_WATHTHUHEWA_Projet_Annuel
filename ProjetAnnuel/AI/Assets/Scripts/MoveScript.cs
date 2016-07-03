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
    float _changeDirectionCoefficient;

    [SerializeField]
    float _acceleration;

    [SerializeField]
    float _deceleration;

    public bool _playerMove;

    Vector3 _lastDirection;


    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
        if(_playerMove)
        {
            // Devant / Derrière
            Vector3 direction = Vector3.zero;

            if (Input.GetKey(KeyCode.UpArrow))
                direction = Vector3.forward;
            if (Input.GetKey(KeyCode.DownArrow))
                direction = Vector3.back;

            if (direction != Vector3.zero)
            {
                direction = direction.normalized;

                if (direction != _lastDirection)
                {
                    _lastDirection = direction;
                    _speed *= _changeDirectionCoefficient;
                }

                if (_speed < _maxSpeed)
                    _speed += (_acceleration * Time.deltaTime);
                else
                    _speed = _maxSpeed;
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


            // Gauche / Droite
            Vector3 rotation = Vector3.zero;

            if (Input.GetKey(KeyCode.RightArrow))
                rotation = Vector3.up;
            if (Input.GetKey(KeyCode.LeftArrow))
                rotation = Vector3.down;

            if (rotation != Vector3.zero)
            {
                _playerTransform.Rotate(rotation * _rotationSpeed * Time.deltaTime, Space.Self);
            }
        }
    }

    
    public void makeMove(PlayerInput playerInput)
    {
        if (!_playerMove && playerInput != null)
        {
            // Devant / Derrière
            Vector3 direction = Vector3.zero;

            if (playerInput._keyCodes.Contains(KeyCode.UpArrow))
                direction = Vector3.forward;
            if (playerInput._keyCodes.Contains(KeyCode.DownArrow))
                direction = Vector3.back;

            if (direction != Vector3.zero)
            {
                direction = direction.normalized;

                if (direction != _lastDirection)
                {
                    _lastDirection = direction;
                    _speed *= _changeDirectionCoefficient;
                }

                if (_speed < _maxSpeed)
                    _speed += (_acceleration * Time.deltaTime);
                else
                    _speed = _maxSpeed;
            }

            if (!(playerInput._keyCodes.Contains(KeyCode.UpArrow) || playerInput._keyCodes.Contains(KeyCode.DownArrow)))
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


            // Gauche / Droite
            Vector3 rotation = Vector3.zero;

            if (playerInput._keyCodes.Contains(KeyCode.RightArrow))
                rotation = Vector3.up;
            if (playerInput._keyCodes.Contains(KeyCode.LeftArrow))
                rotation = Vector3.down;

            if (rotation != Vector3.zero)
            {
                _playerTransform.Rotate(rotation * _rotationSpeed * Time.deltaTime, Space.Self);
            }
        }
    }
}
