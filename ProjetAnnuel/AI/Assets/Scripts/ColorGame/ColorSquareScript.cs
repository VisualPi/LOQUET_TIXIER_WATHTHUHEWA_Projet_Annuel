using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class ColorSquareScript : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> _board;

    [SerializeField]
    public int _boardWidth;

    [SerializeField]
    public int _boardHeight;

    [SerializeField]
    public Material _defaultMaterial;

    [SerializeField]
    public Material _playerOneMaterial;

	// Use this for initialization
	void Start () 
    {
        ResetBoard();
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
                Renderer renderer = go.GetComponent<Renderer>();
                if(renderer.material != _playerOneMaterial)
                    renderer.material = _playerOneMaterial;
            }
        }
    }

    public void ResetBoard()
    {
        foreach(GameObject go in _board)
        {
            Renderer renderer = go.GetComponent<Renderer>();
            if (transform.position.x != go.transform.position.x || transform.position.z != go.transform.position.z)
                renderer.material = _defaultMaterial;
        }
    }
}
