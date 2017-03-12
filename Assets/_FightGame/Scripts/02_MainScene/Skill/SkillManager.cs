using UnityEngine;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour
{
    private static SkillManager _instance;
    public static SkillManager instance
    {
        get
        {
            if (!_instance)
            {
                GameObject skillManager = new GameObject("Default Skill Manager");
                _instance = skillManager.AddComponent<SkillManager>();
            }
            return _instance;
        }
    }

    private Dictionary<int, Skill> skillExecDict;

    void OnEnable()
    {
        skillExecDict = new Dictionary<int, Skill>();

        NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.TriggerSkill);
        NotificationCenter.DefaultCenter.AddObserver(this, StateMachineEvent.OnceActionChange);
    }

    void Update()
    {

    }

    void TriggerSkill(NotificationCenter.Notification info)
    {
        //需要做队列来执行吗？目前先这样吧
        SkillTransferInfo skillInfo = (SkillTransferInfo)info.data;
        FootmanCharacter player = SceneManager.instance.GetCharacterById(skillInfo.playerId);
        player.TriggerSkill(skillInfo.levelInfo.id);

        Skill skill = new Skill();
        skill.Caster(skillInfo.playerId);
        skillExecDict[skillInfo.playerId] = skill;

    }

    public void OnceActionChange(NotificationCenter.Notification info)
    {
        int playerId = (int)info.data;
        FootmanCharacter player = SceneManager.instance.GetCharacterById(playerId);
        player.OnceActionChange();
    }
}
