using UnityEngine;
using System.Collections;

public class HandleSkillBtnsInfo : MonoBehaviour {

    [SerializeField]
    private GameObject skillButton;

	// Use this for initialization
	void Start () {
        initBtns();
	}
	
	void initBtns()
    {
        foreach (SkillItem item in LoginUserInfo.userCareer.skills)
        {
            GameObject cbtn = Instantiate(skillButton, Vector3.zero, Quaternion.identity, this.transform) as GameObject;
            SkillButton carBtn = cbtn.GetComponent<SkillButton>();
            carBtn.btnskillInfo = item;
        }
    }
}
