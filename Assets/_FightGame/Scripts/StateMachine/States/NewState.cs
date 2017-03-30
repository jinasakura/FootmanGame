using UnityEngine;
using System.Collections;

public abstract class NewState  {

    protected Animator animator;
    protected StateMachineParams stateParams;
    public NewState(Animator ani)
    {
        animator = ani;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void HandleParamers(object info)
    {
        stateParams = (StateMachineParams)info;
    }

}
