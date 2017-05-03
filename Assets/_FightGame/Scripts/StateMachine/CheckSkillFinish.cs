using UnityEngine;
using System.Collections;

public class CheckSkillFinish : StateMachineBehaviour {

    private FootmanStateMachine role;
    private int skillId;
    private int loopTimes;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (role == null) { role = animator.gameObject.GetComponentInParent<FootmanStateMachine>(); }
        role.OnSkillState(true);
        if (role.stateParams.skillId != skillId)
        {
            loopTimes = 1;
        }
        else { loopTimes += 1; }
        skillId = role.stateParams.skillId;
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (role.stateParams.loopTimes == loopTimes)
        {
            role.stateParams.isSkill = false;
            animator.SetBool(SkillRef.isSkill, false);
            role.OnSkillState(false);
        }
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMachineEnter is called when entering a statemachine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){
    //
    //}

    // OnStateMachineExit is called when exiting a statemachine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
    //
    //}
}
