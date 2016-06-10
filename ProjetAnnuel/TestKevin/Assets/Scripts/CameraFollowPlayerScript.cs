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

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        float x = _targetTransform.position.x + _distanceX;
        float y = _targetTransform.position.y + _distanceZ;
        float z = _targetTransform.position.z + _distanceY;
        _cameraTransform.position = new Vector3(x, y, z);
	}
}
