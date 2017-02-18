using UnityEngine;
using System.Collections;

public class StruckState : OnceActionState {

    private Animator animator;
    private StateMachineParams stateParams;

    void Awake () {
        animator = GetComponentInChildren<Animator>();
        stateParams = GetComponent<StateMachineParams>();
        this.name = "StruckState";
    }

    //protected override void AddListeners()
    //{
    //    NotificationCenter.DefaultCenter.AddObserver(this, StateMachineEvent.HandleParamers);
    //}

    //protected override void RemoveListeners()
    //{
    //    NotificationCenter.DefaultCenter.RemoveObserver(this, StateMachineEvent.HandleParamers);
    //}

    //void HandleParamers(NotificationCenter.Notification info)
    //{
    //    animator.SetBool("isLive", stateParams.isLive);
    //}
}
