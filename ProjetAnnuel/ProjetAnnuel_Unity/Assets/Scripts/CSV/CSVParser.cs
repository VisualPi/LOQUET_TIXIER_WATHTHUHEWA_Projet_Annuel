using System.Collections;
using UnityEngine;

public class csvPareser : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		CSVReader.DebugOutputGrid(CSVReader.SplitCsvGrid(@"C:\Users\Jonathan\Desktop\sample.csv"));
	}

	// Update is called once per frame
	void Update () {
	
	}
}
