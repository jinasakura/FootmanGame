using UnityEngine;
using System;


/// <summary>
/// 所有切换状态机都在这个类里完成
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class FootmanCharacter : MonoBehaviour
{

    private CharacterStateMachine stateMachine;
    //private bool onceActionBegain = false;

    //private bool _isLive = true;
    public bool isLive
    {
        get { return stateParams.isLive; }
        set
        {
            //_isLive = value;
            stateParams.isLive = value;
        }
    }

    //private int _stayState = 0;
    public int stayState
    {
        get { return stateParams.stayState; }
        set
        {
            //_stayState = value;
            stateParams.stayState = value;
        }
    }

    //private int _onceActionType = 1;
    public int onceActionType
    {
        get { return stateParams.onceActionType; }
        set
        {
            //_onceActionType = value;
            stateParams.onceActionType = value;
        }
    }

    //private float _speed = 0.0f;
    public float speed
    {
        get { return stateParams.speed; }
        set
        {
            //_speed = value;
            stateParams.speed = value;
        }
    }

    private PlayerInfo _playerInfo;
    public PlayerInfo playerInfo { set; get; }

    private StateMachineParams stateParams;

    private HealthSlider healthSlider;
    private HealthSlider mpSlider;

    private CapsuleCollider bodyCollider;
    private CapsuleCollider swordCollider;


    void Start()
    {
        //if (LoginUserInfo.playerInfo.playerId == playerInfo.playerId)
        //{
        //    NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.TriggerSkill);

        //}

        //NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.CharacterDie);
        //NotificationCenter.DefaultCenter.AddObserver(this, StateMachineEvent.OnceActionChange);

        stateParams = GetComponent<StateMachineParams>();
        stateParams.playerId = playerInfo.playerId;
        stateParams.playerName = playerInfo.playerName;

        HealthSlider[] sliders = GetComponentsInChildren<HealthSlider>();
        foreach (HealthSlider slider in sliders)
        {
            if (slider.name == "HealthSlider") healthSlider = slider;
            else mpSlider = slider;
        }
        healthSlider.maxValue = CareerModel.GetLevelItem(playerInfo.careerId, playerInfo.level).hp;
        mpSlider.maxValue = CareerModel.GetLevelItem(playerInfo.careerId, playerInfo.level).mp;

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

    public void Move(float h, float v)
    {
        //时刻记得释放技能和移动是冲突的
        if (stateParams.onceActionBegain)
        {
            stateParams.Idle();
        }
        else
        {
            float tmpH = Mathf.Abs(h);
            float tmpV = Mathf.Abs(v);
            if (tmpH <= PlayerDetail.StayOffset && tmpV <= PlayerDetail.StayOffset)
            {
                stateParams.speed = 0;
            }
            else
            {
                stateParams.moveVelocity = transform.right * h + transform.forward * v;
                stateParams.speed = Mathf.Max(tmpH, tmpV);
            }
            //Debug.Log("---状态机前---playerId->" + playerInfo.playerId + "===speed->" + stateParams.speed);
        }

    }

    //为外界准备的调用
    public void TakeDamage(int amount)
    {
        healthSlider.TakeDamage(amount);

        if (playerInfo.playerId == LoginUserInfo.playerInfo.playerId)
            NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.UserHpChange, amount);
    }

    public void TriggerSkill(int skillId)
    {
        if (stateParams.onceActionBegain) return;

        //SkillLevelItem skill = (SkillLevelItem)skillInfo.data;
        stateParams.ChangeOnceAction(skillId);
    }

    public void Die()
    {
        //int id = Convert.ToInt32(info.data);
        //if (playerInfo.playerId == id)
        stateParams.Die();
    }

    public void OnceActionChange()
    {
        //StateMachineParams param = (StateMachineParams)info.data;
        //if (param.playerId == playerInfo.playerId)
        //{
        if (stateParams.onceActionBegain) swordCollider.enabled = true;
        else swordCollider.enabled = false;//没有砍人时也要关闭
        //}
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && stateParams.onceActionBegain)
        {
            GameObject enemy = other.gameObject;
            FootmanCharacter person = enemy.GetComponent<FootmanCharacter>();
            if (SkillModel.GetSkillById(playerInfo.careerId, onceActionType) != null)
            {
                SkillLevelItem skillLevel = SkillModel.GetSkillById(playerInfo.careerId, onceActionType);

                if (!skillLevel.passive)
                {
                    if (mpSlider.currentValue >= skillLevel.mp)
                    {
                        mpSlider.TakeDamage(skillLevel.mp);

                        if (playerInfo.playerId == LoginUserInfo.playerInfo.playerId)
                            NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.UserMpChange, skillLevel.mp);

                        int damage = skillLevel.damage;
                        person.TakeDamage(damage);
                    }
                    else
                    {
                        Debug.Log("You do not have sufficient mp");
                    }
                }

                swordCollider.enabled = false;
            }
        }
    }


}

