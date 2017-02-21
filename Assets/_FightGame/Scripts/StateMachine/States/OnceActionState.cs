using UnityEngine;
using System.Collections;

public class OnceActionState : State
{

    private Animator animator;
    private StateMachineParams stateParams;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        stateParams = GetComponent<StateMachineParams>();
        this.name = stateParams.playerId+"->OnceActionState";
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
        //if (stateParams == null)
        //    Debug.Log(stateParams.playerId + "丢失数据");
        if (!stateParams.isLive)
            animator.SetBool("isLive", stateParams.isLive);
        animator.SetInteger("onceActionType", stateParams.onceActionType);
        if (stateParams.triggerOnceAction)
        {
            animator.SetTrigger("triggerOnceAction");
            stateParams.triggerOnceAction = false;
        }
        //Debug.Log("状态机里技能id->" + stateParams.onceActionType);
    }


}
