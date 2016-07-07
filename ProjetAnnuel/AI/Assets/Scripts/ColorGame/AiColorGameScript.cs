using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class AiColorGameScript : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _board;

    [SerializeField]
    Material _defaultMaterial;

    [SerializeField]
    Material _playerOneMaterial;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Cube"))
        {
            GameObject go = other.gameObject;
            GameObject parentGo = go.transform.parent.gameObject;

            string[] row = other.gameObject.name.Split('-');
            string[] column = parentGo.name.Split('-');

            if (row.Length > 0 && column.Length > 0)
            {
                //Debug.Log("Case : " + row[1] + "-" + column[1]);

                Renderer renderer = go.GetComponent<Renderer>();
                if(renderer.material != _playerOneMaterial)
                    renderer.material = _playerOneMaterial;
            }
            //else
                //Debug.Log("Case");
        }
    }
}
