using UnityEngine;
using System.Collections;

public class CameraFollowPlayerScript : MonoBehaviour 
{
    [SerializeField]
    Transform _cameraTransform;

    [SerializeField]
    Transform _targetTransform;

    [SerializeField]
    float _distanceX;
    [SerializeField]
    float _distanceY;
    [SerializeField]
    float _distanceZ;

    [SerializeField]
    float _rotationX;
    [SerializeField]
    float _rotationY;
    [SerializeField]
    float _rotationZ;

	// Use this for initialization
	void Start () 
    {
        _cameraTransform.Rotate(new Vector3(_rotationX, _rotationY, _rotationZ));
	}
	
	// Update is called once per frame
	void LateUpdate () 
    {
        /*
        float x = _targetTransform.position.x + _distanceX;
        float y = _targetTransform.position.y + _distanceZ;
        float z = _targetTransform.position.z + _distanceY;
        _cameraTransform.position = new Vector3(x, y, z);
        _cameraTransform.LookAt(_targetTransform);
        */

        _cameraTransform.position = _targetTransform.position - (_targetTransform.forward * 8) + (_targetTransform.up * 8);

        _cameraTransform.LookAt(_targetTransform.position + _targetTransform.forward*4);
        
	}
}
