using UnityEngine;
using System.Collections;

public class SkillState : RoleState
{

    public override void HandleParamers(object info)
    {
        base.HandleParamers(info);
        if (!stateParams.isLive)
            animator.SetBool(SkillRef.isLive, stateParams.isLive);
        animator.SetInteger(SkillRef.skillId, stateParams.skillId);
        animator.SetTrigger(SkillRef.triggerSkill);
        if (stateParams.triggerSkill)
        {
            //animator.SetTrigger("triggerOnceAction");
            stateParams.triggerSkill = false;
        }
    }


}
