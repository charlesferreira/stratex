using UnityEngine;

public class PulseIdleState : StateMachineBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.GetComponent<Pulse>().OnAnimationFinish();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.GetComponent<Pulse>().OnAnimationStart();
    }
}
