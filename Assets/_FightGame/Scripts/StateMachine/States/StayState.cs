using UnityEngine;
using System.Collections;

public class StayState : State {

    private Animator animator;
    public StayState(Animator ani)
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
        animator.SetInteger("stayState", data.stayState);
    }

}
