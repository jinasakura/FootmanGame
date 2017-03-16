using UnityEngine;
using System.Collections;

public class StayState : RoleState
{

    //private Animator animator;
    //private StateMachineParams stateParams;

    //void Awake()
    //{
    //    animator = GetComponentInChildren<Animator>();
    //    stateParams = GetComponent<StateMachineParams>();
    //    this.name = stateParams.playerId + "StayState";
    //}

    //protected override void AddListeners()
    //{
    //    NotificationCenter.DefaultCenter.AddObserver(this, StateMachineEvent.HandleParamers);
    //}

    //protected override void RemoveListeners()
    //{
    //    NotificationCenter.DefaultCenter.RemoveObserver(this, StateMachineEvent.HandleParamers);
    //}

    protected override void HandleParamers()
    {
        if (!stateParams.isLive)
            animator.SetBool("isLive", stateParams.isLive);
        animator.SetInteger("stayState", stateParams.stayState);
    }


}
