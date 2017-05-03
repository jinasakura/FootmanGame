using UnityEngine;
using System.Collections;

public class StayState : RoleState
{
    //public StayState(Animator ani):base(ani)
    //{
    //    animator = ani;
    //}

    //public override void Enter()
    //{
    //    base.Enter();
    //    Debug.Log("静止");
    //}

    public override void HandleParamers(object info)
    {
        base.HandleParamers(info);
        if (!stateParams.isLive)
            animator.SetBool(SkillRef.isLive, stateParams.isLive);
        animator.SetInteger(SkillRef.stayState, stateParams.stayState);
    }


}
