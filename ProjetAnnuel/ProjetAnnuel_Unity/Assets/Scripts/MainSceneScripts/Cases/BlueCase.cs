using UnityEngine;
using System.Collections;

public class BlueCase : AbstractCase
{
	void Start()
	{
		SetNbPlayerOnCase(0);
	}

	void Update()
	{

	}

    public override void ApplyEffect(int playerID)
    {
        //Not implemented yet
        //Utils.Instance.GetPlayerByID(playerID).AddPoints(50) 
    }
}
