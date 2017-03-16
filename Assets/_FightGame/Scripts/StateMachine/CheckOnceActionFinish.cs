using UnityEngine;
using System.Collections;

public class CheckOnceActionFinish : StateMachineBehaviour {

    private FootmanSkill skill;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StateMachineParams param = animator.gameObject.GetComponentInParent<StateMachineParams>();
        //param.onceActionBegain = true;
        //PlayerInfo player = animator.gameObject.GetComponentInParent<PlayerInfo>();
        //NotificationCenter.DefaultCenter.PostNotification(animator, StateMachineEvent.OnceActionChange, player.playerId);
        if (skill == null) { skill = animator.gameObject.GetComponentInParent<FootmanSkill>(); }
        skill.OnSkillStateChange(true);
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StateMachineParams param = animator.gameObject.GetComponentInParent<StateMachineParams>();
        //param.onceActionBegain = false;
        skill.OnSkillStateChange(false);
        //PlayerInfo player = animator.gameObject.GetComponentInParent<PlayerInfo>();
        //NotificationCenter.DefaultCenter.PostNotification(animator, StateMachineEvent.OnceActionChange, player.playerId);
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
