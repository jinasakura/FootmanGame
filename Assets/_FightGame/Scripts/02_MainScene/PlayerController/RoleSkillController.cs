using UnityEngine;
using System;


/// <summary>
/// 职责：接受释放技能的命令后，判断应该创建哪个skill类
/// </summary>
public class RoleSkillController : MonoBehaviour
{
    public Action<int> OnSkillTrigger { set; get; }

    public enum Attack { CloseSingle = 1, FarSingle , CloseGroup ,FarGroup ,CloseGround, FarGround };
    public enum Passive { CureSelf = 1 ,CureOther , CureGroup ,DefenseSelf ,DefenseGroup };

    private PlayerInfo playerInfo;
    private RoleSkill skill;

    void Start()
    {
        ReleaseSkillMode();
    }

    //从哪里收到释放技能的命令（还没释放呢！）
    protected virtual void ReleaseSkillMode()
    {
        playerInfo = GetComponent<PlayerInfo>();
        if (playerInfo.playerId == LoginUserInfo.playerId)
        {
            NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.ReleaseSkill);
        }
    }

    public void ReleaseSkill(NotificationCenter.Notification info)
    {
        string skillName = (string)info.data;
        SkillLevelItem skillLevel = SkillModel.GetSkillLevelByName(skillName);
        if (skillLevel.CheckCondition(playerInfo.detail))
        {
            if (!skillLevel.passive)
            {
                Attack type = (Attack)skillLevel.skillType;
                switch (type)
                {
                    case Attack.CloseSingle:
                        skill = gameObject.AddComponent<ACloseSingleSkill>();
                        break;
                    case Attack.CloseGroup:
                        break;

                }
            }
            else
            {
                Passive type = (Passive)skillLevel.skillType;
                switch (type)
                {
                    case Passive.CureSelf:
                        skill = gameObject.AddComponent<PCureSelfSkill>();
                        break;
                }
            }
            skill.skillLevel = skillLevel;
            Action<int> localOnChange = OnSkillTrigger;
            if (localOnChange != null)
            {
                localOnChange(skillLevel.id);
            }
        }
        else
        {
            Debug.Log("技能  "+skillName + "  不符合释放条件");
        }
    }

}
