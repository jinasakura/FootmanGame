using UnityEngine;
using System.Collections;

public class FootmanRoleFight : MonoBehaviour
{

    private PlayerInfo playerInfo;

    private SimpleColorSlider healthSlider;
    private SimpleColorSlider mpSlider;

    //private CapsuleCollider bodyCollider;
    //private CapsuleCollider swordCollider;
    private CapsuleCollider[] colliders;

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

        colliders = GetComponentsInChildren<CapsuleCollider>();
        //foreach (CapsuleCollider item in colliders)
        //{
        //    if (item.gameObject.tag == "Weapon")
        //    {
        //        swordCollider = item;
        //    }
        //    else
        //    {
        //        bodyCollider = item;
        //    }
        //}
    }

    public void TriggerSkill(SkillLevelItem info)
    {
        float amount = info.damageHp;
        healthSlider.UpdateValue(amount);
        playerInfo.detail.DeductHp(amount);

        if (playerInfo.playerId == LoginUserInfo.playerInfo.playerId)
            NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.UserHpChange, amount);
    }

    //一个动作期间：是否要区分主、被动技能
    public void OnSkillStateChange(bool begain)
    {
        skillBegain = begain;
        if (skillBegain)
        {
            ChangeColliderState(true);
        }
        else//没有砍人时也要关闭
        {
            ChangeColliderState(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (skillBegain && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (other.gameObject.tag == "Body")
            {
                GameObject enemy = other.gameObject;
                FootmanStateMachine person = enemy.GetComponent<FootmanStateMachine>();
            }
            else if(other.gameObject.tag == "Weapon")
            {

            }

        }
    }

    private void ChangeColliderState(bool state)
    {
        foreach (CapsuleCollider collider in colliders)
        {
            collider.enabled = state;
        }
    }
}
