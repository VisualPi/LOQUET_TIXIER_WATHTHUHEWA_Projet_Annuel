using UnityEngine;
using System.Collections;

public class Cinematique : MonoBehaviour
{
	public Camera camera;

	private Vector3 startMarker;
	private Vector3 endMarker;
	private float length;
	private float startTime;

	private bool isAnimFinished;

	public void OnAnimEnd()
	{
		camera.cullingMask = -1;
		GetComponent<Animator>().speed = 0.5f;
	}

	public void OnAnimFinished()
	{
		GetComponent<Animator>().speed = 0f;
		isAnimFinished = true;
	}

	public bool GetCinematiqueFinished()
	{
		return isAnimFinished;
	}
    public void SetCinematiqueFinished(bool value)
    {
        isAnimFinished = value;
    }

    public void OnIncreaseSpeed()
	{
		GetComponent<Animator>().speed = 1;
	}

}
