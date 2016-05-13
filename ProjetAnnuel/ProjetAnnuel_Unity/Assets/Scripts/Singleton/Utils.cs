using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour
{
	public Material blue;
	public Material black;
	public Material red;
	public Material green;
	public Material yellow;
	public Material purple;

	private static Utils instance = null;
	public static Utils Instance { get { return instance; } }

	void Awake()
	{
		instance = this;
	}
}
