using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour {

    private SkillItem _btnSkillInfo;
    public SkillItem btnskillInfo
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
        object info = btnskillInfo;
        NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.TriggerSkill, info);
    }
}
