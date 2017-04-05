using UnityEngine;
using System.Collections;

public class PCureSelfSkill : RoleSkill {

    void Update()
    {
        if (skillFire)
        {
            playerInfo.detail.AddHp(skillLevel.mp);
        }
    }
}
