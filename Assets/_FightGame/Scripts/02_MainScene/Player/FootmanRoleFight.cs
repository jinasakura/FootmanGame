using UnityEngine;
using System.Collections;


/// <summary>
/// (废弃)
/// </summary>
public class FootmanRoleFight : MonoBehaviour
{
    
    private PlayerInfo playerInfo;

    private SimpleColorSlider healthSlider;
    private SimpleColorSlider mpSlider;

    //private CapsuleCollider bodyCollider;
    private Collider _weaponCollider;
    public Collider weaponCollider { private set; get; }
    //private CapsuleCollider[] colliders;

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

        Collider[] colliders = GetComponentsInChildren<CapsuleCollider>();
        foreach (Collider item in colliders)
        {
            if (item.gameObject.tag == "Weapon")
            {
                weaponCollider = item;
                weaponCollider.enabled = false;
            }
        }
    }


    public void TriggerSkill(float mp)
    {
        //skillInfo = info;
        //float amount = skillInfo.damageHp;
        mpSlider.UpdateValue(mp);
        playerInfo.detail.DeductMp(mp);
        //character.TakeDamageAction();

        //if (playerInfo.playerId == LoginUserInfo.playerInfo.playerId)
        //{
        //    NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.UserMpChange, mp);
        //    LoginUserInfo.playerInfo.detail.DeductMp(mp);
        //}
    }

    //一个动作期间：是否要区分主、被动技能
    public void OnSkillStateChange(bool begain)
    {
        skillBegain = begain;
        if (skillBegain)
        {
            ChangeWeaponColliderState(weaponCollider, true);
        }
        else//没有砍人时也要关闭
        {
            
            ChangeWeaponColliderState(weaponCollider, false);
        }
    }

    //public bool CheckSkillTouched()
    //{
    //    return checkTouch.touched;
    //}

    //public int CheckTouchedSkillId()
    //{
    //    return checkTouch.skillId;
    //}

    //这里只检测受击对象
    void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.tag+"   "+ skillBegain);
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Debug.Log("攻击中");
            GameObject enemy = other.gameObject;//以武器所在gameobject为中心
            //CheckActionTouch checkTouch = enemy.GetComponentInParent<CheckActionTouch>();
            UserCameraController role = enemy.GetComponentInParent<UserCameraController>();
            //if (role && role.CheckTouchedInFight())
            //{
            //    Debug.Log("peng");
            //}
            //if (checkTouch != null && role != null)
            //{
            //    //Debug.Log("值 ！= null");
            //    if (role.skillInfo.id == checkTouch.skillId)//确定碰撞的同一个技能
            //    {
            //        FootmanRoleFight fight = enemy.GetComponentInParent<FootmanRoleFight>();
            //        if (fight != null)
            //        {
            //            //Debug.Log("打中");
            //            ChangeWeaponColliderState(fight.weaponCollider, false);
            //            TakeDamage(role.skillInfo.damageHp);
            //            //enemy.GetComponentInParent<FootmanStateMachine>().TakeDamageAction();
            //        }
            //    }
            //}
        }
    }


    private void TakeDamage(float amount)
    {
        healthSlider.UpdateValue(amount);
        playerInfo.detail.DeductHp(amount);
        //if (playerInfo.playerId == LoginUserInfo.playerInfo.playerId)
        //{
        //    NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.UserMpChange, amount);
        //    LoginUserInfo.playerInfo.detail.DeductHp(amount);
        //}
    }

    public void ChangeWeaponColliderState(Collider weapon, bool state)
    {
        //把bodyCollider关了，人就掉下去了
        weapon.enabled = state;
    }
}
