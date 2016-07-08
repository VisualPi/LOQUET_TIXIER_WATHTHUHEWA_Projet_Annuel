using UnityEngine;
using System.Collections;

public class PlayerMoveState : StateMachineBehaviour
{
	private Player _currentPlayer;
	private Vector3 _startMarker;
	private Vector3 _endMarker;

	[SerializeField]
	private float _speed = 20;

	private int _iteration;
	private bool ok = false;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
	{
		ok = false;
		_iteration = -1;
		_currentPlayer = Utils.Instance.GetPlayerByColor(animator.GetComponent<GameManager>().GetCurrentPlayer());
		_currentPlayer.ResetAnim();
		_currentPlayer.GetAnimator().Play("running_inPlace");
	}
	
	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
	{
		int currentIteration = Mathf.FloorToInt(stateInfo.normalizedTime);
		if (currentIteration != _iteration)
		{
			_iteration = currentIteration;
			if( currentIteration == animator.GetComponent<GameManager>().GetDiceNumber() )
			{
				Debug.Log("iter = " + currentIteration);
				_currentPlayer.GetAnimator().Play("idle");
				animator.GetComponent<CameraManager>().UnfocusOnPlayer(_currentPlayer.GetPlayerColor());
				animator.GetComponent<GameManager>().NextPlayer();
				animator.SetTrigger("PlayerFinishMoveTrigger");
				ok = true; // NANDEEE
				return;
			}
			else
			{
				var nextID = Utils.Instance.GetCaseByID(_currentPlayer.GetCaseID()).GetNextCaseID();
				_currentPlayer.SetCaseID(nextID);
                if (Utils.Instance.GetCaseByID(nextID).GetCaseType() == ECaseType.INTERSECTION)
                    animator.GetComponent<GameManager>().SetDiceNumber(animator.GetComponent<GameManager>().GetDiceNumber() + 1);

                _startMarker = _currentPlayer.transform.position;
				_endMarker = Utils.Instance.GetCaseByID(nextID).GetCasePosition(_currentPlayer.GetPlayerColor(), false);
				float animLength = Vector3.Distance(_startMarker, _endMarker);
                
                switch (_currentPlayer.GetPlayerColor())
                {
                    case EPlayer.BLUE:
                        PlayerPrefs.SetInt("PLAYER_BLUE_CASEID", nextID);
                        break;
                    case EPlayer.GREEN:
                        PlayerPrefs.SetInt("PLAYER_GREEN_CASEID", nextID);
                        break;
                    case EPlayer.RED:
                        PlayerPrefs.SetInt("PLAYER_RED_CASEID", nextID);
                        break;
                    case EPlayer.YELLOW:
                        PlayerPrefs.SetInt("PLAYER_YELLOW_CASEID", nextID);
                        break;
                }


                animator.SetFloat("MoveInverseDuration", _speed / animLength);
			}
		}
		if(!ok )//TODO : A voir !!
		{
			float fracJourney = stateInfo.normalizedTime % 1.0f;
			_currentPlayer.transform.position = Vector3.Lerp(_startMarker, _endMarker, fracJourney);
			_currentPlayer.transform.LookAt(_endMarker);
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
