using UnityEngine;
using System.Collections;

public class PlayerStartPointingState : StateMachineBehaviour
{

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
	{
		Utils.Instance.GetPlayerByColor(animator.GetComponent<GameManager>().GetCurrentPlayer()).PlayAnimPointing();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
	{
		if( Utils.Instance.GetPlayerByColor(animator.GetComponent<GameManager>().GetCurrentPlayer()).GetAnimPointMiddle() )
		{
			animator.SetTrigger("WheelFreezeTrigger");
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
