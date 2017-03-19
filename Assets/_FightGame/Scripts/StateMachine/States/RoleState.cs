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
        //stateParams = GetComponent<StateMachineParams>();
    }

    public override void HandleParamers(object info)
    {
        stateParams = (StateMachineParams)info;
    }

}
