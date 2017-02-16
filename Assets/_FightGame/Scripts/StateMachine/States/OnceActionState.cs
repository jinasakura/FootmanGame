using UnityEngine;
using System.Collections;

public class OnceActionState : State {

    private Animator animator;
    private StateMachineParams stateParams;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        stateParams = GetComponent<StateMachineParams>();
        this.name = "OnceActionState";
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
        animator.SetInteger("onceActionType", stateParams.onceActionType);
        if (stateParams.triggerOnceAction)
        {
            animator.SetTrigger("triggerOnceAction");
            stateParams.triggerOnceAction = false;
        }
        //Debug.Log("状态机里技能id->" + data.onceActionType);
    }


}
