using UnityEngine;
using System;

public class NormalAttackSkill : RoleSkill
{

    
    protected SkillLevelItem skillLevel;

    protected override void ReleaseSkillMode()
    {
        if (playInfo.playerId == LoginUserInfo.playerInfo.playerId)
        {
            NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.ReleaseSkill);
        }
    }

    protected override void init()
    {
        base.init();
        

        //stateMachine = GetComponent<RoleStateMachine>();
        //this.OnSkillTrigger += stateMachine.TriggerSkill;

        actionFire = GetComponentInChildren<SkillActionFire>();
        actionFire.OnSkillFired += this.OnSkillFire;


    }

    public void ReleaseSkill(NotificationCenter.Notification info)
    {
        string skillName = (string)info.data;
        skillLevel = SkillModel.GetSkillLevelByName(skillName);
        if (skillLevel.CheckCondition(playInfo.detail))
        {
            //TriggerSkillMode(item);
            OnSkillTrigger(skillLevel.id);
        }
    }

    protected override void OnSkillTriggerHandler(Collider enemyCollider)
    {
        //base.OnSkillTriggerHandler(other);
        if (skillFire)
        {
            //从受击者角度来判断碰撞
            if (enemyCollider.gameObject.layer == LayerMask.NameToLayer("Player")
                && enemyCollider.gameObject.tag == "Weapon")
            {
                PlayerInfo enemyInfo = enemyCollider.gameObject.GetComponentInParent<PlayerInfo>();
                enemyInfo.detail.DeductMp(skillLevel.mp);
                playInfo.detail.DeductHp(skillLevel.damageHp);
                RoleStateMachine role = gameObject.GetComponent<RoleStateMachine>();
                if (role != null)
                {
                    role.TakeDamageAction();
                }
            }
        }
    }
}
