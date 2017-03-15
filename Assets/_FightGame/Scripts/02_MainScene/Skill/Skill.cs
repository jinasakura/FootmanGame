using System;
using UnityEngine;

public class Skill : MonoBehaviour {

    private SkillLevelItem _skillInfo;
    public SkillLevelItem skillInfo { set; get; }

    private int casterPlayerId;
    private FootmanCharacter charater;

    public void Caster(int playerId)
    {
        casterPlayerId = playerId;
        charater = SceneManager.instance.GetCharacterById(casterPlayerId);
    }


    public bool CheckCondition(PlayerDetailInfo playerRequirement)
    {
        bool use = false;
        if (skillInfo.level <= playerRequirement.level)
        {
            use = true;
            if (playerRequirement.currentMp >= skillInfo.mp)
            {
                use = true;
            }
            else { use = false; }
        }
        else { use = false; }
        return use;
    }

    public void Trigger()
    {
        if (skillInfo.skillType == Convert.ToInt16(SkillTpye.Attack.Single) || 
            skillInfo.skillType == Convert.ToInt16(SkillTpye.Attack.Group))
        {
            charater.TriggerSkill(skillInfo);
        }
        
    }

    public void OnSkillStateChange()
    {
        charater.OnSkillStateChange();
    }
}
