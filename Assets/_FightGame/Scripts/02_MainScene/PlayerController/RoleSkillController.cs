using UnityEngine;
using System;


/// <summary>
/// 职责：接受释放技能的命令后，判断应该创建哪个skill类
/// </summary>
public class RoleSkillController : MonoBehaviour
{
    public Action<int> OnSkillTrigger { set; get; }

    //CloseSingle-接触攻击单人，FarSingle-某距离内攻击单人，CloseGroup-接触攻击某范围内群体，FarGroup-远程攻击某范围内群体
    //CloseGround-(再议)，FarGround-再议
    public enum Attack { CloseSingle = 1, FarSingle , CloseGroup ,FarGroup ,CloseGround, FarGround };
    //CureSelf-治愈自己，CureOther-治愈他人（这个人离我的远近？），DefenseSelf-自己加防御，DefenseGroup-防御某范围内一群人
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
        int skillId = (int)info.data;
        SkillLevelItem skillInfo = SkillModel.GetSkillLevelByName(skillId);
        if (skillInfo.CheckCondition(playerInfo.detail))
        {
            if (!skillInfo.passive)
            {
                Attack type = (Attack)skillInfo.skillType;
                switch (type)
                {
                    case Attack.CloseSingle:
                        skill = GetSkill<ACloseSingleSkill>();
                        break;
                    case Attack.FarSingle:
                        skill = GetSkill<AMeleeAttackSkill>();
                        break;

                }
            }
            else
            {
                Passive type = (Passive)skillInfo.skillType;
                switch (type)
                {
                    case Passive.CureSelf:
                        skill = GetSkill<PCureSelfSkill>();
                        break;
                }
            }
            skill.skillInfo = skillInfo;
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
        }
        return target;
    }

}
