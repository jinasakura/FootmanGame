using UnityEngine;
using System;
using System.Collections;


/// <summary>
/// （4.3）还是先不要急着搞继承关系，先把功能捋清楚了，再来总结父类有什么功能
/// </summary>
public class RoleSkill : MonoBehaviour
{

    protected PlayerInfo playerInfo;
    protected Collider weaponCollider;
    protected SkillActionFire actionFire;
    protected bool skillFire = false;
    public SkillLevelItem skillLevel;

    void Start()
    {
        init();
    }

    protected virtual void init()
    {
        playerInfo = GetComponent<PlayerInfo>();
        
        Collider[] colliders = GetComponentsInChildren<CapsuleCollider>();
        foreach (Collider item in colliders)
        {
            if (item.gameObject.tag == "Weapon")
            {
                weaponCollider = item;
                item.enabled = false;//只能把武器的关了，body关了人就掉下去了
            }
        }

        actionFire = GetComponentInChildren<SkillActionFire>();
        actionFire.OnSkillFired += this.OnSkillFire;
    }

    protected virtual void OnSkillFire(bool fire)
    {
        //Debug.Log("skillFire"+fire);
        skillFire = fire;
        weaponCollider.enabled = fire;
    }

    protected virtual void CloseWeapon()
    {
        weaponCollider.enabled = false;
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    OnSkillTriggerHandler(other);
    //}

    //protected virtual void OnSkillTriggerHandler(Collider other)
    //{

    //}
}
