using UnityEngine;
using System.Collections;

public class OnceActionState : State {

    private Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

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
        StateMachineParams data = (StateMachineParams)info.data;
        animator.SetBool("isLive", data.isLive);
        animator.SetInteger("onceActionType", data.onceActionType);
        if (data.triggerOnceAction)
        {
            animator.SetTrigger("triggerOnceAction");
            data.triggerOnceAction = false;
        }
        Debug.Log("状态机里技能id->" + data.onceActionType);
    }


}
