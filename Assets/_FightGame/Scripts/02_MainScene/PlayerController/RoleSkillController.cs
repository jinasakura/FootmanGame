using UnityEngine;
using System;
using System.Collections.Generic;


/// <summary>
/// 职责：接受释放技能的命令后，判断应该创建哪个skill类
/// 技能类型不能和职业挂钩，而应该与技能特点挂钩
/// </summary>
public class RoleSkillController : MonoBehaviour
{
    public Action<int> OnSkillTrigger { set; get; }

    private PlayerInfo playerInfo;
    private RoleSkill skill;

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        ReleaseSkillMode();
    }

    //从哪里收到释放技能的命令（还没释放呢！）
    protected virtual void ReleaseSkillMode()
    {
        if (playerInfo.playerId == LoginUserInfo.playerId)
        {
            NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.ReleaseSkill);
        }
    }

    public void ReleaseSkill(NotificationCenter.Notification info)
    {
        int skillId = (int)info.data;
        SkillLevelItem skillInfo = SkillModel.GetSkillLevelByName(skillId);
        CancelAllSubscriber();
        if (skillInfo.CheckCondition(playerInfo.detail))
        {
            if (!skillInfo.passive)
            {
                SkillRef.Attack type = (SkillRef.Attack)skillInfo.skillType;
                switch (type)
                {
                    case SkillRef.Attack.TouchPerson:
                        skill = GetSkill<ACloseSingleSkill>();
                        break;
                    case SkillRef.Attack.UniObject:
                        skill = GetSkill<LancherSkill>();
                        break;

                }
            }
            else
            {
                SkillRef.Passive type = (SkillRef.Passive)skillInfo.skillType;
                switch (type)
                {
                    case SkillRef.Passive.CureSelf:
                        skill = GetSkill<PCureSelfSkill>();
                        break;
                }
            }
            skill.skillInfo = skillInfo;
            skill.SubscribActionFire();
            Action<int> localOnChange = OnSkillTrigger;
            if (localOnChange != null)
            {
                localOnChange(skillInfo.id);
            }
        }
        else
        {
            Debug.Log("技能  "+skillInfo.skillName + "  不符合释放条件");
        }
    }

    private T GetSkill<T>() where T : RoleSkill
    {
        T target = gameObject.GetComponent<T>();
        if (target == null)
        {
            target = gameObject.AddComponent<T>();
            gameObject.name = playerInfo.playerName;
            target.init();
        }
        return target;
    }

    private void CancelAllSubscriber()
    {
        RoleSkill[] ary = GetComponents<RoleSkill>();
        foreach (RoleSkill item in ary)
        {
            item.CancelSubscrib();
        }
    }

}
