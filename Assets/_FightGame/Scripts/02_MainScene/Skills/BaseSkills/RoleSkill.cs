using UnityEngine;
using System;
using System.Collections.Generic;


/// <summary>
/// （4.3）还是先不要急着搞继承关系，先把功能捋清楚了，再来总结父类有什么功能
/// </summary>
public class RoleSkill : MonoBehaviour
{

    protected PlayerInfo playerInfo;
    //protected Collider weaponCollider;
    
    protected SkillActionFire actionFire;
    protected bool skillFireStart = false;
    private SkillLevelItem _skillInfo;
    public SkillLevelItem skillInfo { protected get; set; }


    public virtual void init()
    {
        playerInfo = GetComponent<PlayerInfo>();
        actionFire = GetComponentInChildren<SkillActionFire>();
    }

    public void SubscribActionFire()
    {
        actionFire.OnSkillFired += this.OnSkillFire;
    }

    public void CancelSubscrib()
    {
        actionFire.OnSkillFired -= this.OnSkillFire;
    }

    protected virtual void OnSkillFire(int skillId, bool fire)
    {
        //Debug.Log(skillInfo.skillName+"----skillFire->" + fire);
        skillFireStart = fire;
    }



}
