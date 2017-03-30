using UnityEngine;

public class DieState : NewState
{
    public DieState(Animator ani) : base(ani)
    {
        animator = ani;
    }

    public override void HandleParamers(object info)
    {
        base.HandleParamers(info);
        animator.SetBool("isLive", stateParams.isLive);
    }


}
