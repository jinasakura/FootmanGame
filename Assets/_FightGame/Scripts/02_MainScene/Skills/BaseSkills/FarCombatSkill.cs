using UnityEngine;
using System.Collections;


/// <summary>
/// 远程职业的技能的基类
/// </summary>
public class FarCombatSkill : RoleSkill {

    protected Transform firePoint;
    //protected Transform rolePoint;

    public override void init()
    {
        base.init();
        string lanchPoint = SkillRef.FirePointTag + skillInfo.firePoint.ToString();
        if (GameObject.Find(lanchPoint) != null) {
            firePoint = GameObject.Find(lanchPoint).transform;
        }
        else { Debug.LogWarning("找不到lanchPoint->" + lanchPoint); }
        //rolePoint = GetComponentInParent<PlayerInfo>().gameObject.transform;
    }

}
