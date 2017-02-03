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
        if (LoginUserInfo.userCareer.skills == null)
        {
            Debug.Log("没有技能数据，无法创建技能按钮！");
        }
        else
        {
            foreach (SkillItem item in LoginUserInfo.userCareer.skills)
            {
                GameObject cbtn = Instantiate(skillButton, Vector3.zero, Quaternion.identity, this.transform) as GameObject;
                SkillButton carBtn = cbtn.GetComponent<SkillButton>();
                carBtn.btnskillInfo = item;
            }
        }
        
    }
}
