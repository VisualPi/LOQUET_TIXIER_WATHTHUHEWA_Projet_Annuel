using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class ResultManagerScene : MonoBehaviour {

    [SerializeField]
    AudioClip MusicResult;

    [SerializeField]
    AudioClip ApplauseSound;

    [SerializeField]
    AudioClip EndingMusicResult;

    
    AudioSource audioSource;

    public bool isFinalResult;

	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();

        if (!isFinalResult)
        {
            audioSource.PlayOneShot(MusicResult);
        }
        else
        {
            audioSource.PlayOneShot(EndingMusicResult);
        }



	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(Time.timeSinceLevelLoad);
        if(Time.timeSinceLevelLoad >= 5)
        {
            Debug.Log("YOLO");
            SceneManager.LoadScene(1);
        }

	}
}
