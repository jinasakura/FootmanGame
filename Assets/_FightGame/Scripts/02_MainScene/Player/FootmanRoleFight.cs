using UnityEngine;
using System.Collections;

public class FootmanRoleFight : MonoBehaviour
{

    private PlayerInfo playerInfo;

    private SimpleColorSlider healthSlider;
    private SimpleColorSlider mpSlider;

    private CapsuleCollider bodyCollider;
    private CapsuleCollider swordCollider;

    private bool skillBegain;

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();

        SimpleColorSlider[] sliders = GetComponentsInChildren<SimpleColorSlider>();
        foreach (SimpleColorSlider slider in sliders)
        {
            if (slider.name == "HealthSlider") healthSlider = slider;
            else mpSlider = slider;
        }

        CareerLevelItem careerLevel = CareerModel.GetLevelItem(playerInfo.careerId, playerInfo.detail.level);
        healthSlider.maxValue = careerLevel.hp;
        mpSlider.maxValue = careerLevel.mp;

        playerInfo.detail.currentHp = careerLevel.hp;
        playerInfo.detail.currentMp = careerLevel.mp;

        CapsuleCollider[] colliders = GetComponentsInChildren<CapsuleCollider>();
        foreach (CapsuleCollider item in colliders)
        {
            if (item.gameObject.tag == "Weapon")
            {
                swordCollider = item;
            }
            else
            {
                bodyCollider = item;
            }
        }
    }

    public void TriggerSkill(SkillLevelItem info)
    {
        playerInfo.detail.DeductMp(info.mp);
    }

    //一个动作期间：是否要区分主、被动技能
    public void OnSkillStateChange(bool begain)
    {
        skillBegain = begain;
        if (skillBegain)
        {
            swordCollider.enabled = true;
            bodyCollider.enabled = true;
        }
        else//没有砍人时也要关闭
        {
            swordCollider.enabled = false;
            bodyCollider.enabled = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (skillBegain && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (other.gameObject.tag == "Body")
            {
                GameObject enemy = other.gameObject;
                FootmanCharacter person = enemy.GetComponent<FootmanCharacter>();
            }
            else if(other.gameObject.tag == "Weapon")
            {

            }

        }
    }
}
