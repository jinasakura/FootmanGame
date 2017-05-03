using UnityEngine;
using System.Collections;

public class SkillState : RoleState
{
    //public OnceActionState(Animator ani) : base(ani)
    //{
    //    animator = ani;
    //}

    public override void HandleParamers(object info)
    {
        base.HandleParamers(info);
        if (!stateParams.isLive)
            animator.SetBool(SkillRef.isLive, stateParams.isLive);
        animator.SetInteger(SkillRef.skillId, stateParams.skillId);
        animator.SetBool(SkillRef.isSkill, stateParams.isSkill);
        //SkillActionFire actionFire = GetComponent<SkillActionFire>();
        //if (stateParams.loopTimes == actionFire.loopTimes)//stateParams.loopTimes == actionFire.loopTimes
        //{
        //    //animator.SetTrigger("triggerOnceAction");
        //    //stateParams.triggerOnceAction = false;
        //    stateParams.isSkill = false;
        //}
    }


}
