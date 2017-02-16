using UnityEngine;

public class DieState : State
{
    private Animator animator;
    private StateMachineParams stateParams;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        stateParams = GetComponent<StateMachineParams>();
        this.name = "DieState";
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
        //StateMachineParams data = (StateMachineParams)info.data;
        animator.SetBool("isLive", stateParams.isLive);
    }


}
