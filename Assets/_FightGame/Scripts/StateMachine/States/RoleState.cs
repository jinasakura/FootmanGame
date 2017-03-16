using UnityEngine;
using System.Collections;

public class RoleState : State {

    protected Animator animator;
    protected StateMachineParams stateParams;

    void Awake () {
        init();
	}

    protected virtual void init()
    {
        animator = GetComponentInChildren<Animator>();
        stateParams = GetComponent<StateMachineParams>();
    }

    protected override void AddListeners()
    {
        NotificationCenter.DefaultCenter.AddObserver(this, StateMachineEvent.HandleParamers);
    }

    protected override void RemoveListeners()
    {
        NotificationCenter.DefaultCenter.RemoveObserver(this, StateMachineEvent.HandleParamers);
    }

    protected virtual void HandleParamers()
    {

    }

}
