using UnityEngine;
using System;

public class SkillState : RoleState
{

    public override void HandleParamers(object info)
    {
        base.HandleParamers(info);
        if (!stateParams.isLive)
            animator.SetBool(SkillRef.isLive, stateParams.isLive);
        animator.SetInteger(SkillRef.skillId, stateParams.skillId);
        animator.SetTrigger(SkillRef.triggerSkill);
        //TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        //Debug.Log("&&&"+ts);
        if (stateParams.triggerSkill)
        {
            //animator.SetTrigger("triggerOnceAction");
            stateParams.triggerSkill = false;
        }
    }


}
