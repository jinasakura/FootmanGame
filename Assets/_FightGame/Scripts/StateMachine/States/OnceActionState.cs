using UnityEngine;
using System.Collections;

public class OnceActionState : State {

    private Animator animator;
    public OnceActionState(Animator ani)
    {
        animator = ani;
        AddListeners();
    }

    protected override void AddListeners()
    {
        NotificationCenter.DefaultCenter.AddObserver(this, StateMachineEvent.HandleParamers);
    }

    protected override void RemoveListeners()
    {
        NotificationCenter.DefaultCenter.RemoveObserver(this, StateMachineEvent.HandleParamers);
    }

    void HandleParamers(NotificationCenter.Notification info)
    {
        StateMachineParams data = (StateMachineParams)info.data;
        animator.SetBool("isLive", data.isLive);
        animator.SetInteger("onceActionType", data.onceActionType);
        if (data.triggerOnceAction)
        {
            animator.SetTrigger("triggerOnceAction");

            //data.triggerOnceAction = false;
            //object recoverData = data;
            //NotificationCenter.DefaultCenter.PostNotification(this, StateMachineEvent.HandleParamers, recoverData);
        }
    }
}
