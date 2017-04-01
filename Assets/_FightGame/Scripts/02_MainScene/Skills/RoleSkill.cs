using UnityEngine;
using System;
using System.Collections;

public class RoleSkill : MonoBehaviour
{
    public Action<int> OnSkillTrigger { set; get; }

    protected PlayerInfo playInfo;
    protected Collider weaponCollider;
    //protected RoleStateMachine stateMachine;
    protected SkillActionFire actionFire;
    protected bool skillFire = false;
    //public Collider weaponCollider { private set; get; }

    void Start()
    {
        init();
        ReleaseSkillMode();
    }

    protected virtual void init()
    {
        playInfo = GetComponent<PlayerInfo>();

        Collider[] colliders = GetComponentsInChildren<CapsuleCollider>();
        foreach (Collider item in colliders)
        {
            if (item.gameObject.tag == "Weapon")
            {
                weaponCollider = item;
                item.enabled = false;//只能把武器的关了，body关了人就掉下去了
            }
        }
    }

    //从哪里收到释放技能的命令（还没释放呢！）
    protected virtual void ReleaseSkillMode()
    {

    }

    //protected virtual void TriggerSkillMode(SkillLevelItem item)
    //{

    //}

    protected virtual void OnSkillFire(bool fire)
    {
        skillFire = fire;
        weaponCollider.enabled = fire;
    }

    void OnTriggerEnter(Collider other)
    {
        OnSkillTriggerHandler(other);
    }

    protected virtual void OnSkillTriggerHandler(Collider other)
    {

    }
}
