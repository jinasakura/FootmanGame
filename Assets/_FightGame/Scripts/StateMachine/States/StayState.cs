using UnityEngine;
using System.Collections;

public class StayState : State {

    private Animator animator;
    private StateMachineParams stateParams;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        stateParams = GetComponent<StateMachineParams>();
        this.name = "StayState";
    }

    protected override void AddListeners()
    {
        NotificationCenter.DefaultCenter.AddObserver(this, StateMachineEvent.HandleParamers);
    }

    protected override void RemoveListeners()
    {
        NotificationCenter.DefaultCenter.RemoveObserver(this, StateMachineEvent.HandleParamers);
    }

    void HandleParamers()
    {
        //StateMachineParams data = (StateMachineParams)info.data;
        animator.SetBool("isLive", stateParams.isLive);
        animator.SetInteger("stayState", stateParams.stayState);
    }


}
