using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	private List<int> playersCaseID;//list contenant l'ID de la case de chaque joueur dans l'ordre de l'enum EPlayer. Par exemple playersCaseID[0] correspondra a la case sur laquelle est le joueur 0 (bleu)

	private EPlayer _currentPlayer;


	void Start()
	{
		playersCaseID = new List<int>(4);
		for( int i = 0 ; i < 4 ; i++ )
			playersCaseID.Insert(i, 0);
		Utils.Instance.GetPlayerByColor(EPlayer.BLUE).transform.position = Utils.Instance.GetDepartCase().GetCasePosition(EPlayer.BLUE);
		Utils.Instance.GetPlayerByColor(EPlayer.GREEN).transform.position = Utils.Instance.GetDepartCase().GetCasePosition(EPlayer.GREEN);
		Utils.Instance.GetPlayerByColor(EPlayer.RED).transform.position = Utils.Instance.GetDepartCase().GetCasePosition(EPlayer.RED);
		Utils.Instance.GetPlayerByColor(EPlayer.YELLOW).transform.position = Utils.Instance.GetDepartCase().GetCasePosition(EPlayer.YELLOW);
		_currentPlayer = EPlayer.BLUE;//TODO:choisir par l'ordre aléatoire (lancé de dé)
	}
	private int diceNumber = -1;//nombre de déplacement choisi par le dé
	private int currentNumber = 1;//nombre actuel de déplacement
	bool diceChoosed = false;
	void Update()
	{
		if( diceChoosed )
		{
			if( diceNumber == -1 )
			{
				diceChoosed = false;
			}
			else
			{
				if( Utils.Instance.GetPlayerByColor(_currentPlayer).IsAnimStarted() )
				{
					if( Utils.Instance.GetPlayerByColor(_currentPlayer).IsPositionReached() )
					{
						Debug.Log("Position reached");
						Utils.Instance.GetPlayerByColor(_currentPlayer).SetAnimStarted(false);
						if( currentNumber == diceNumber )
						{
							diceNumber = -1;
							currentNumber = 1;
							_currentPlayer = NextPlayer();
							diceChoosed = false;
						}
						else
							currentNumber++;
					}
				}
				else
				{
					playersCaseID[(int)_currentPlayer] = Utils.Instance.GetCaseByID(playersCaseID[(int)_currentPlayer]).GetNextCaseID(); //Met l'id de la case actuelle dans la list a la position currentPlayer
					Utils.Instance.GetPlayerByColor(_currentPlayer).InitAnim(Utils.Instance.GetCaseByID(playersCaseID[(int)_currentPlayer]).GetCasePosition(_currentPlayer));
					//Debug.Log("Init Anim case : " + playersCaseID[(int)_currentPlayer] + " , to pos : " + Utils.Instance.GetCaseByID(playersCaseID[(int)_currentPlayer]).GetCasePosition(_currentPlayer));
				}

				//Utils.Instance.GetPlayerByColor(_currentPlayer).transform.position = Utils.Instance.GetCaseByID(playersCaseID[(int)_currentPlayer]).GetCasePosition(_currentPlayer);

			}
		}

	}

	public EPlayer NextPlayer()//TODO: faire un ordre aleatoire en début de partie
	{
		switch( _currentPlayer )
		{
		case EPlayer.BLUE:
			return EPlayer.GREEN;
		case EPlayer.GREEN:
			return EPlayer.RED;
		case EPlayer.RED:
			return EPlayer.YELLOW;
		case EPlayer.YELLOW:
			return EPlayer.BLUE;
		default:
			return EPlayer.BLUE;
		}
	}

	public void OnDiceClick( bool value )
	{
		diceNumber = Random.Range(1, 6);
		Debug.Log("dice number : " + diceNumber);
		diceChoosed = true;
	}
}
