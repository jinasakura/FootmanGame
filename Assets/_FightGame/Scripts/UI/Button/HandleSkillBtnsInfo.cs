using UnityEngine;
using System.Collections;

public class HandleSkillBtnsInfo : MonoBehaviour {

    [SerializeField]
    private GameObject skillButton;

    void Start () {
        initBtns();
	}
	
	void initBtns()
    {
        if (LoginUserInfo.skillLevels == null)
        {
            Debug.Log("没有技能数据，无法创建技能按钮！");
        }
        else
        {
            foreach (SkillLevelItem item in LoginUserInfo.skillLevels)
            {
                GameObject cbtn = Instantiate(skillButton, Vector3.zero, Quaternion.identity, this.transform) as GameObject;
                SkillButton carBtn = cbtn.GetComponent<SkillButton>();
                carBtn.skillName = item.skillName;
            }
        }
        
    }
}
