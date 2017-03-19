using UnityEngine;
using System.Collections;

public class OnceActionState : RoleState
{
    public override void HandleParamers(object info)
    {
        base.HandleParamers(info);
        if (!stateParams.isLive)
            animator.SetBool("isLive", stateParams.isLive);
        animator.SetInteger("onceActionType", stateParams.onceActionType);
        if (stateParams.triggerOnceAction)
        {
            animator.SetTrigger("triggerOnceAction");
            stateParams.triggerOnceAction = false;
        }
    }


}
