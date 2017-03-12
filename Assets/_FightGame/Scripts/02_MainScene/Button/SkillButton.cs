﻿using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour {

    private SkillLevelItem _btnSkillInfo;
    public SkillLevelItem btnskillInfo
    {
        set
        {
            _btnSkillInfo = value;
            Text txt = GetComponentInChildren<Text>();
            txt.text = _btnSkillInfo.skillName;
        }
        get { return _btnSkillInfo; }
    }

    private Button btn;
    private Text btnTxt;
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnTriggerSkill);
    }

    void OnTriggerSkill()
    {
        //Debug.Log("SkillButton->释放技能id：" + btnskillInfo.skillId);
        SkillTransferInfo info = new SkillTransferInfo();
        info.playerId = LoginUserInfo.playerInfo.playerId;
        info.levelInfo = btnskillInfo;
        NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.TriggerSkill, info);
    }
}
