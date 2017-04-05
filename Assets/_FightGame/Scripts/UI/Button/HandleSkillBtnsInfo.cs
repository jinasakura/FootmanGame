using UnityEngine;
using System.Collections.Generic;

public class HandleSkillBtnsInfo : MonoBehaviour {

    [SerializeField]
    private GameObject skillButton;

    void Start () {
        initBtns();
	}
	
	void initBtns()
    {
        List<SkillLevelItem> items = SkillModel.GetAllSkillType();
        if (items == null)
        {
            Debug.Log("没有技能数据，无法创建技能按钮！");
        }
        else
        {
            foreach (SkillLevelItem item in items)
            {
                GameObject cbtn = Instantiate(skillButton, Vector3.zero, Quaternion.identity, this.transform) as GameObject;
                SkillButton carBtn = cbtn.GetComponent<SkillButton>();
                carBtn.skillName = item.skillName;
            }
        }
        
    }
}
