using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TextAnimation : MonoBehaviour
{
	[SerializeField]
	private Text _text;
	[SerializeField]
	private Animator _animation;

	private bool isAnimTextFinished;

	public void OnTextAnimFinished()
	{
		isAnimTextFinished = true;
	}
	public bool GetAnimFinished()
	{
		return isAnimTextFinished;
	}
	public void Reset()
	{
		isAnimTextFinished = false;
	}
	public void SetText(string text)
	{
		_text.text = text;
	}
	public void PlayAnim()
	{
		if (_text.text == "TURN")//oui c'est deg !!!!
			_animation.Play("TextTurnTranslate");
		else
			_animation.Play("TextTranslate");
	}
}
