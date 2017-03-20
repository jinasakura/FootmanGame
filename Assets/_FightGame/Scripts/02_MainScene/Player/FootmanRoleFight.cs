using UnityEngine;
using System.Collections;

public class FootmanRoleFight : MonoBehaviour
{
    //private SkillLevelItem _skillInfo;
    //public SkillLevelItem skillInfo {private set; get; }

    private PlayerInfo playerInfo;

    private SimpleColorSlider healthSlider;
    private SimpleColorSlider mpSlider;

    //private CapsuleCollider bodyCollider;
    private CapsuleCollider swordCollider;
    private CapsuleCollider[] colliders;

    private bool _skillBegain;
    public bool skillBegain { private set; get; }

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();

        SimpleColorSlider[] sliders = GetComponentsInChildren<SimpleColorSlider>();
        foreach (SimpleColorSlider slider in sliders)
        {
            if (slider.name == "HealthSlider") healthSlider = slider;
            else mpSlider = slider;
        }

        healthSlider.maxValue = playerInfo.detail.currentHp;
        mpSlider.maxValue = playerInfo.detail.currentMp;

        colliders = GetComponentsInChildren<CapsuleCollider>();
        foreach (CapsuleCollider item in colliders)
        {
            if (item.gameObject.tag == "Weapon") swordCollider = item;
        }
    }


    public void TriggerSkill(float mp)
    {
        //skillInfo = info;
        //float amount = skillInfo.damageHp;
        mpSlider.UpdateValue(mp);
        playerInfo.detail.DeductMp(mp);

        if (playerInfo.playerId == LoginUserInfo.playerInfo.playerId)
        {
            NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.UserMpChange, mp);
            LoginUserInfo.playerInfo.detail.DeductMp(mp);
        }
    }

    //一个动作期间：是否要区分主、被动技能
    public void OnSkillStateChange(bool begain)
    {
        skillBegain = begain;
        if (skillBegain)
        {
            ChangeColliderState(true);
            //Debug.Log("开始技能");
        }
        else//没有砍人时也要关闭
        {
            
            ChangeColliderState(false);
            //Debug.Log("结束技能");
        }
    }

    //这里只检测受击对象
    void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.tag+"   "+ skillBegain);
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (other.gameObject.tag == "Weapon")
            {
                //Debug.Log(other.gameObject.tag);
                ChangeColliderState(false);
                GameObject enemy = other.gameObject;
                FootmanRoleController role = enemy.GetComponentInParent<FootmanRoleController>();
                SkillLevelItem enemySkill = role.skillInfo;
                TakeDamage(enemySkill.damageHp);
            }
        }
    }

    void TouchEnemy(int skillId)
    {
        Debug.Log("攻击：" + skillId);
    }

    private void TakeDamage(float amount)
    {
        healthSlider.UpdateValue(amount);
        playerInfo.detail.DeductHp(amount);
        if (playerInfo.playerId == LoginUserInfo.playerInfo.playerId)
        {
            NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.UserMpChange, amount);
            LoginUserInfo.playerInfo.detail.DeductHp(amount);
        }
    }

    private void ChangeColliderState(bool state)
    {
        //foreach (CapsuleCollider collider in colliders)
        //{
        //    collider.enabled = state;
        //}
        swordCollider.enabled = state;
    }
}
