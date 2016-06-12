using UnityEngine;
using System.Collections.Generic;

public class Utils : MonoBehaviour
{
	public Player playerBlue;   //Player1
	public Player playerGreen;  //Player2
	public Player playerRed;    //Player3
	public Player playerYellow; //Player4

	public AbstractCase departCase;
	public List<AbstractCase> allCases;

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
	void Start()
	{
		departCase.SetCaseID(0);
		for (var i = 0; i < allCases.Count; i++)
			allCases[i].SetCaseID(i + 1); //met l'ID de chaque case a leur position+1 dans la liste
	}

	public Player GetPlayerByID(int id)
	{
		switch (id)
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
	public Player GetPlayerByColor(EPlayer color)
	{
		switch (color)
		{
			case EPlayer.BLUE:
				return playerBlue;
			case EPlayer.GREEN:
				return playerGreen;
			case EPlayer.RED:
				return playerRed;
			case EPlayer.YELLOW:
				return playerYellow;
			default:
				return null;
		}
	}
	public AbstractCase GetDepartCase()
	{
		return departCase;
	}
	public AbstractCase GetCaseByID(int value)
	{
		if (value == 0)
			return departCase;
		else
			return allCases[value-1];//-1 car on met l'id a l'indice de la liste +1 dans le start
	}
}
