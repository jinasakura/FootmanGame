﻿using UnityEngine;
using System;

public class CheckSkillFinish : StateMachineBehaviour
{

    private PlayerStateMachine role;
    private int skillId;
    //private int loopTimes;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (role == null) { role = animator.gameObject.GetComponentInParent<PlayerStateMachine>(); }
        role.OnSkillState(true);
        if (role.stateParams.skillId == (int)SkillRef.SkillType.TakeDamage) { return; }
        if (role.stateParams.skillId != skillId)
        {
            skillId = role.stateParams.skillId;
        }
        role.stateParams.curLoopTimes += 1;
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (role.stateParams.skillId == (int)SkillRef.SkillType.TakeDamage)
        {
            role.OnSkillState(false);
            return;
        }
        if (role.stateParams.curLoopTimes == role.stateParams.totalLoopTimes)
        {
            role.OnSkillState(false);
            role.stateParams.curLoopTimes = 0;
        }
        else
        {
            role.stateParams.triggerSkill = true;
            role.ChangeState<SkillState>();
            //TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            //Debug.Log(ts);
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
