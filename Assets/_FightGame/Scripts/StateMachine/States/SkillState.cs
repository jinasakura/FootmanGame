using UnityEngine;
using System;

public class SkillState : RoleState
{

    public override void HandleParamers(object info)
    {
        base.HandleParamers(info);
        if (!stateParams.isLive)
            animator.SetBool(RoleRef.isLive, stateParams.isLive);
        animator.SetInteger(RoleRef.skillId, stateParams.skillId);
        animator.SetTrigger(RoleRef.triggerSkill);
        //TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        //Debug.Log("&&&"+ts);
        if (stateParams.triggerSkill)
        {
            //animator.SetTrigger("triggerOnceAction");
            stateParams.triggerSkill = false;
        }
    }


}
