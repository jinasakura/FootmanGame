using UnityEngine;

public class DieState : State
{
    private Animator animator;
    public DieState(Animator ani)
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
    }
}
