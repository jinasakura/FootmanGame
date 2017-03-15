using UnityEngine;
using System;


/// <summary>
/// 所有切换状态机都在这个类里完成
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class FootmanCharacter : MonoBehaviour
{

    //private CharacterStateMachine stateMachine;

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


    private PlayerInfo playerInfo;
    //public PlayerInfo playerInfo { set; get; }

    private StateMachineParams stateParams;

    private SimpleColorSlider healthSlider;
    private SimpleColorSlider mpSlider;

    private CapsuleCollider bodyCollider;
    private CapsuleCollider swordCollider;

    private SkillLevelItem skillInfo;

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();
        stateParams = GetComponent<StateMachineParams>();
        //stateParams.playerId = playerInfo.playerId;
        //stateParams.playerName = playerInfo.playerName;

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

    public void Move(float h, float v)
    {
        //时刻记得释放技能和移动是冲突的
        if(!stateParams.onceActionBegain)
        {
            float tmpH = Mathf.Abs(h);
            float tmpV = Mathf.Abs(v);
            if (tmpH <= PlayerDetail.StayOffset && tmpV <= PlayerDetail.StayOffset)
            {
                stateParams.moveVelocity = Vector3.zero;
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

    public void Die()
    {
        stateParams.Die();
    }

    public void Live()
    {
        stateParams.Live();
    }

    //为外界准备的调用
    public void TakeDamage(int amount)
    {
        healthSlider.TakeDamage(amount);
        playerInfo.detail.DeductHp(amount);

        if (playerInfo.playerId == LoginUserInfo.playerInfo.playerId)
            NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.UserHpChange, amount);
    }

    public void TriggerSkill(SkillLevelItem info)
    {
        stateParams.ChangeOnceAction(info.id);
        playerInfo.detail.DeductMp(info.mp);
    }


    //一个动作期间：是否要区分主、被动技能
    public void OnSkillStateChange()
    {
        if (stateParams.onceActionBegain)
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
        if (stateParams.onceActionBegain && other.gameObject.tag == "Player")
        {
            GameObject enemy = other.gameObject;
            FootmanCharacter person = enemy.GetComponent<FootmanCharacter>();
            //我怎么知道打到对方的bodyCollider上了，去对方身上判断

        }
    }


}

