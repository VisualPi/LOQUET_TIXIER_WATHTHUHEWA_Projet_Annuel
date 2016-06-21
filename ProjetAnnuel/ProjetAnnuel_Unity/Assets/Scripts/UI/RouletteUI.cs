using UnityEngine;
using System.Collections;

public class RouletteUI : MonoBehaviour
{
	[SerializeField]
	private Roulette _roulette;

	private int nbRoulette;

	public void SetNbRoulette(string value)
	{
		nbRoulette = int.Parse(value);
	}

	public void RoulettoStarto()
	{
		_roulette.StartSpin();
	}
	public void StopRoulette()
	{
		_roulette.StopSpin(nbRoulette);
	}


}
