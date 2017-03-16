using System;
using UnityEngine;

public class FootmanSkill : MonoBehaviour {

    private SkillLevelItem _skillInfo;
    public SkillLevelItem skillInfo { set; get; }

    private int casterPlayerId;
    private FootmanCharacter charater;
    private FootmanRoleFight fight;

    private bool _skillBegain;
    public bool skillBegain { set; get; }

    void Start()
    {
        charater = GetComponent<FootmanCharacter>();
        fight = GetComponent<FootmanRoleFight>();
    }

    public void ReleaseSkill(string skillName)
    {
        skillInfo = SkillModel.GetSkillLevelByName(skillName);
     }

    //public void Caster(int playerId)
    //{
    //    casterPlayerId = playerId;
    //    charater = SceneManager.instance.GetCharacterById(casterPlayerId);
    //}


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
            charater.TriggerSkill(skillInfo.id);
            fight.TriggerSkill(skillInfo);
        }
        
    }

    public void OnSkillStateChange(bool begain)
    {
        skillBegain = begain;
        fight.OnSkillStateChange(skillBegain);
    }
}
