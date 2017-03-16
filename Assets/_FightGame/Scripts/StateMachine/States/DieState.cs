using UnityEngine;

public class DieState : RoleState
{
    //private Animator animator;
    //private StateMachineParams stateParams;

    //void Awake()
    //{
    //    animator = GetComponentInChildren<Animator>();
    //    stateParams = GetComponent<StateMachineParams>();
    //    //this.name = stateParams.playerId+"DieState";
    //}

    protected override void AddListeners()
    {
        NotificationCenter.DefaultCenter.AddObserver(this, StateMachineEvent.HandleParamers);
    }

    protected override void RemoveListeners()
    {
        NotificationCenter.DefaultCenter.RemoveObserver(this, StateMachineEvent.HandleParamers);
    }

    private void HandleParamers()
    {
        animator.SetBool("isLive", stateParams.isLive);
        //Debug.Log(stateParams.playerId + "->Die");
    }


}
