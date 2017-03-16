using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour {

    // UI操作输入
    // Skill逻辑信息存储

    // 1. 对象职责单一
    // 2. 对象职责明确
    // 3. 视图、操作、逻辑分离。MVC。模型视图控制器。模型：数据的存储以及读写管理；视图：界面以及界面的操作获取；控制器：业务逻辑

    //private SkillLevelItem _btnSkillInfo;
    //public SkillLevelItem btnskillInfo
    //{
    //    set
    //    {
    //        _btnSkillInfo = value;
    //        Text txt = GetComponentInChildren<Text>();
    //        txt.text = _btnSkillInfo.skillName;
    //    }
    //    get { return _btnSkillInfo; }
    //}

    private string _skillName;
    public string skillName { set; get; }

    private Button btn;
    private Text btnTxt;
    void Start()
    {
        Text txt = GetComponentInChildren<Text>();
        txt.text = skillName;

        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnTriggerSkill);
    }

    void OnTriggerSkill()
    {
        //SkillTransferInfo info = new SkillTransferInfo();
        //info.playerId = LoginUserInfo.playerInfo.playerId;
        //info.levelInfo = btnskillInfo;
        //NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.ReleaseSkill, skillName);
        GetComponent<FootmanSkill>().ReleaseSkill(skillName);
    }
}
