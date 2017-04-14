using UnityEngine;
using System.Collections;

public class PCureSelfSkill : RoleSkill
{
    private bool isAttack = false;

    void Update()
    {
        if (skillFireStart && !isAttack)
        {
            playerInfo.detail.AddHp(skillInfo.healHp);
            playerInfo.detail.AddMp(skillInfo.healMp);
            isAttack = true;
        }
    }
}
