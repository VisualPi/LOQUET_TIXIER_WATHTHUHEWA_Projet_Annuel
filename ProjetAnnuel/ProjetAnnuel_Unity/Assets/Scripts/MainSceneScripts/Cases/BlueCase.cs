using UnityEngine;
using System.Collections;

public class BlueCase : AbstractCase
{

	void Start()
	{
		GetMeshRenderer().material = Utils.Instance.blue;
	}

	void Update()
	{

	}
}
