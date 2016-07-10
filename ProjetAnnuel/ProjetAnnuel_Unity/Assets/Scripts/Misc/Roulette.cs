using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Roulette : MonoBehaviour
{
	[SerializeField]
	private Transform _transform;

	private List<int> xRots;
	void Start()
	{
		xRots = new List<int>(9);
		xRots.Insert(0, 180);     //1
		xRots.Insert(1, 220);   //2
		xRots.Insert(2, 260);   //3
		xRots.Insert(3, 300);  //4
		xRots.Insert(4, 340);  //5
		xRots.Insert(5, 380);  //6
		xRots.Insert(6, 420);  //7
		xRots.Insert(7, 460);  //8
		xRots.Insert(8, 500);  //9
	}
	private bool isStarted;
	float x;
	void Update()
	{
		if (isStarted)
		{
			x += 1080f * Time.deltaTime;
			_transform.rotation = Quaternion.Euler(x, 12f, 180f);
		}
	}

	public void StartSpin()
	{
		x = 0f;
		isStarted = true;
    }

	public void StopSpin(int nb)
	{
		isStarted = false;
		_transform.rotation = Quaternion.Euler(xRots[nb - 1], 12f, 180f);
	}
}
