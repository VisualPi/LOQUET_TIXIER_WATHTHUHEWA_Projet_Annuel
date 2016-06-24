using UnityEngine;
using System.Collections;

public class CameraZoomState : StateMachineBehaviour
{

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
	{
		animator.GetComponent<CameraManager>().GetMainCamera().SetActive(false);
		animator.GetComponent<CameraManager>().GetMainCamera().GetComponent<AudioListener>().enabled = false;
		animator.GetComponent<CameraManager>().GetPlayerCamera().SetActive(true);
		animator.GetComponent<CameraManager>().GetRoulette().gameObject.SetActive(false);
		animator.GetComponent<CameraManager>().GetPlayerCamera().GetComponent<AudioListener>().enabled = true;
		animator.GetComponent<CameraManager>().GetPlayerCanvas().SetActive(true);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate( Animator animator, AnimatorStateInfo stateInfo, int layerIndex )
	{
		var pos = Utils.Instance.GetPlayerByColor(animator.GetComponent<GameManager>().GetCurrentPlayer()).transform.position;
		animator.GetComponent<CameraManager>().GetPlayerCamera().transform.position = Vector3.Lerp(	animator.GetComponent<CameraManager>().GetMainCamera().transform.position, 
																									new Vector3(pos.x + 4, pos.y + 5, pos.z - 15), 
																									stateInfo.normalizedTime % 1.0f);
		
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
