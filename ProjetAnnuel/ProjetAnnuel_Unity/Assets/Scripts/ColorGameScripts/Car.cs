using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _navMesh;

    [SerializeField]
    private Transform _targetPos1;

    [SerializeField]
    private Transform _targetPos2;

    [SerializeField]
    private Transform _targetPos3;

    private Vector3 _defaultPosition;
    private Vector3 _currentDestination;
    private bool sens = true; //true pour le chemin allé, false pour retour

    // Use this for initialization
    void Start()
    {
        _defaultPosition = transform.position;
        _navMesh.SetDestination(_targetPos1.position);
        _currentDestination = _targetPos1.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z); //deg mais je comprends pas pourquoi les voitures sont dans le sol sinon
        if (Mathf.Abs(transform.position.x - _currentDestination.x) < 0.1f && Mathf.Abs(transform.position.z - _currentDestination.z) < 0.1f)
        {
            var nextPos = GetNextPos();
            _navMesh.SetDestination(nextPos);
            _currentDestination = nextPos;
        }
    }

    private Vector3 GetNextPos()
    {
        if(sens)
        {
            if (_currentDestination == _targetPos1.position)
                return _targetPos2.position;
            if (_currentDestination == _targetPos2.position)
                return _targetPos3.position;
            if (_currentDestination == _targetPos3.position)
            {
                sens = !sens;
                return _targetPos2.position;
            }
        }
        else
        {
            if (_currentDestination == _defaultPosition)
            {
                sens = !sens;
                return _targetPos1.position;
            }
            if (_currentDestination == _targetPos1.position)
                return _defaultPosition;
            if (_currentDestination == _targetPos2.position)
                return _targetPos1.position;
        }
        Debug.Log("Oulala");
        return Vector3.zero;
    }
}
