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
		xRots.Insert(0, 0);     //1
		xRots.Insert(1, 320);   //2
		xRots.Insert(2, 280);   //3
		xRots.Insert(3, 240);  //4
		xRots.Insert(4, 200);  //5
		xRots.Insert(5, 160);  //6
		xRots.Insert(6, 120);  //7
		xRots.Insert(7, 80);  //8
		xRots.Insert(8, 40);  //9

	}
	private bool isStarted;
	float x;
	void Update()
	{
		if (isStarted)
		{
			x += 1080f * Time.deltaTime;
			_transform.rotation = Quaternion.Euler(x, 180, 0);
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
		_transform.rotation = Quaternion.Euler(xRots[nb - 1], 180, 0);
	}
}
