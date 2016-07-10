using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	private Vector4 _delimitation = new Vector4(45f, 0f, 0, -45f); //maxX, minX, maxZ, minZ
	public Vector3 nextStep;
	public Vector3 defaultPos;

	private float _startTime = 0f;
	private Vector3 _startMarker;
	private Vector3 _endMarker;
	private float _distance;
	private bool _isMoving = false;

	public void Start()
	{
		nextStep = new Vector3(transform.position.x, 0f, transform.position.z);
		defaultPos = nextStep;
	}

	void Update()
	{
		GetInputs();
		if( _isMoving )
		{
			float speed = _distance / 0.4f; //0.4 est le temps voulu pour parcourir la distance
			float coef = (Time.time - _startTime) * speed;
			transform.position = Vector3.Lerp(_startMarker, _endMarker, coef / _distance);
			if( ( Mathf.Abs(transform.position.x - _endMarker.x) < 0.1f ) && ( Mathf.Abs(transform.position.z - _endMarker.z) < 0.1f ) )
			{
				transform.position = _endMarker;
                GetComponent<Player>().GetAnimator().Play("idle");
				_isMoving = false;
			}
		}
	}

	public void GetInputs()
	{
		if( gameObject.GetComponent<Player>().GetPlayerColor() == EPlayer.BLUE )
		{
			if( Input.GetAxis("Horizontal") == -1 || Input.GetAxis("Horizontal") == 1 )
			{
				nextStep = new Vector3(transform.position.x + ( Input.GetAxis("Horizontal") * 5f ), transform.position.y, transform.position.z);
			}
			if( Input.GetAxis("Vertical") == -1 || Input.GetAxis("Vertical") == 1 )
			{
				nextStep = new Vector3(transform.position.x, transform.position.y, transform.position.z + ( Input.GetAxis("Vertical") * -5f ));
			}
		}

		if( gameObject.GetComponent<Player>().GetPlayerColor() == EPlayer.GREEN )
		{
			if( Input.GetAxis("Horizontal2") == -1 || Input.GetAxis("Horizontal2") == 1 )
			{
				nextStep = new Vector3(transform.position.x + ( Input.GetAxis("Horizontal2") * 5f ), transform.position.y, transform.position.z);
			}
			if( Input.GetAxis("Vertical2") == -1 || Input.GetAxis("Vertical2") == 1 )
			{
				nextStep = new Vector3(transform.position.x, transform.position.y, transform.position.z + ( Input.GetAxis("Vertical2") * 5f ));
			}
		}

		if( gameObject.GetComponent<Player>().GetPlayerColor() == EPlayer.RED )
		{
			if( Input.GetAxis("Horizontal3") == -1 || Input.GetAxis("Horizontal3") == 1 )
			{
				nextStep = new Vector3(transform.position.x + ( Input.GetAxis("Horizontal3") * 5f ), transform.position.y, transform.position.z);
			}
			if( Input.GetAxis("Vertical3") == -1 || Input.GetAxis("Vertical3") == 1 )
			{
				nextStep = new Vector3(transform.position.x, transform.position.y, transform.position.z + ( Input.GetAxis("Vertical3") * 5f ));
			}
		}
		if( gameObject.GetComponent<Player>().GetPlayerColor() == EPlayer.YELLOW )
		{
			if( Input.GetAxis("Horizontal4") == -1 || Input.GetAxis("Horizontal4") == 1 )
			{
				nextStep = new Vector3(transform.position.x + ( Input.GetAxis("Horizontal4") * 5f ), transform.position.y, transform.position.z);
			}
			if( Input.GetAxis("Vertical4") == -1 || Input.GetAxis("Vertical4") == 1 )
			{
				nextStep = new Vector3(transform.position.x, transform.position.y, transform.position.z + ( Input.GetAxis("Vertical4") * 5f ));
			}
		}
		//if (Input.GetKey(KeyCode.Z))
		//{
		//    nextStep = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5f);
		//}
		//if (Input.GetKey(KeyCode.S))
		//{
		//    nextStep = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5f);
		//}
		//if (Input.GetKey(KeyCode.Q))
		//{
		//    nextStep = new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z);
		//}
		//if (Input.GetKey(KeyCode.D))
		//{
		//    nextStep = new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z);
		//}
	}

	public void Move( Vector3 pos )
	{
		if(pos.x != transform.position.x || pos.z != transform.position.z)
		{
			
			_startTime = Time.time;
			_startMarker = transform.position;
			_endMarker = pos;
			_distance = Vector3.Distance(_startMarker, _endMarker);
			_isMoving = true;
			transform.LookAt(_endMarker);
			GetComponent<Player>().GetAnimator().Play("jump");
		}
	}
}
