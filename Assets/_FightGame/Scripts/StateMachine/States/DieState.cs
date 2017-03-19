using UnityEngine;

public class DieState : RoleState
{

    public override void HandleParamers(object info)
    {
        base.HandleParamers(info);
        animator.SetBool("isLive", stateParams.isLive);
    }


}
