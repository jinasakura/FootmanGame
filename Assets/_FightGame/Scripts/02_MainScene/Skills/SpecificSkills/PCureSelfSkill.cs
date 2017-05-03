using UnityEngine;
using System.Collections;

public class PCureSelfSkill : RoleSkill
{
    private bool isAttack = false;


    protected override void OnSkillFire(int skillId,bool fire)
    {
        base.OnSkillFire(skillId,fire);

        if (skillFireStart)
        {
            playerInfo.detail.AddHp(skillInfo.healHp);
            playerInfo.detail.AddMp(skillInfo.healMp);
            //isAttack = true;
        }
    }
}
