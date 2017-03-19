using UnityEngine;
using System.Collections;

public class StayState : RoleState
{

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("静止");
    }

    public override void HandleParamers(object info)
    {
        base.HandleParamers(info);
        if (!stateParams.isLive)
            animator.SetBool("isLive", stateParams.isLive);
        animator.SetInteger("stayState", stateParams.stayState);
    }


}
