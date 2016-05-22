using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour
{
    public Player playerBlue;   //Player1
    public Player playerGreen;  //Player2
    public Player playerRed;    //Player3
    public Player playerYellow; //Player4

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

    public Player GetPlayerByID(int id)
    {
        switch(id)
        {
            case 1:
                return playerBlue;
            case 2:
                return playerGreen;
            case 3:
                return playerRed;
            case 4:
                return playerYellow;
            default:
                return null;
        }
    }
}
