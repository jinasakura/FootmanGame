using UnityEngine;
using System.Collections;

/// <summary>
/// 受击
/// </summary>
public class StruckState : OnceActionState {

    private SimpleColorSlider healthSlider;
    private PlayerInfo playerInfo;
    private FootmanSkill skill;

    protected override void init()
    {
        base.init();

        playerInfo = GetComponent<PlayerInfo>();
        skill = GetComponent<FootmanSkill>();

        SimpleColorSlider[] sliders = GetComponentsInChildren<SimpleColorSlider>();
        foreach (SimpleColorSlider slider in sliders)
        {
            if (slider.name == "HealthSlider") healthSlider = slider;
        }
    }

    protected override void HandleParamers()
    {
        base.HandleParamers();

        float amount = SkillModel.GetSkillById(skill.skillInfo.careerId, skill.skillInfo.id).damageHp;
        healthSlider.TakeDamage(amount);
        playerInfo.detail.DeductHp(amount);

        if (playerInfo.playerId == LoginUserInfo.playerInfo.playerId)
            NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.UserHpChange, amount);
    }
}
