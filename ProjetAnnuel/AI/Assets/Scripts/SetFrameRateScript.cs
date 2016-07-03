using UnityEngine;
using System.Collections;

public class SetFrameRateScript : MonoBehaviour 
{
    [SerializeField]
    int _fps;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
