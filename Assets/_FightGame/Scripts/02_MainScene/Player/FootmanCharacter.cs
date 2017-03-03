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

    [SerializeField]
    private float _currentHp;
    public float currentHp
    {
        set { _currentHp = value; }
        get { return _currentHp; }
    }

    private float maxHealth;

    private CapsuleCollider bodyCollider;
    private CapsuleCollider swordCollider;

    //public int orderInLayer = 0;
    //public string sortingLayerName = "UI-HUD";
    //private Canvas uiCanvas;

    void Start()
    {
        if (LoginUserInfo.playerInfo.playerId == playerInfo.playerId)
        {
            NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.TriggerSkill);
            
        }

        NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.CharacterDie);
        NotificationCenter.DefaultCenter.AddObserver(this, StateMachineEvent.OnceActionChange);
        NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.TakeDamageNotice);

        stateParams = GetComponent<StateMachineParams>();
        stateParams.playerId = playerInfo.playerId;
        stateParams.playerName = playerInfo.playerName;

        healthSlider = GetComponentInChildren<HealthSlider>();
        currentHp = maxHealth = healthSlider.maxValue = CareerModel.GetLevelItem(playerInfo.careerId, playerInfo.level).hp;

        //uiCanvas = GetComponentInChildren<Canvas>();
        ////uiCanvas.worldCamera = Camera.main;
        //uiCanvas.sortingOrder = orderInLayer;
        //uiCanvas.sortingLayerName = sortingLayerName;

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

        //Debug.Log("playerId->" + playerInfo.playerId + "---rotation->" + transform.rotation.eulerAngles);
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

    //void Update()
    //{
    //    Debug.Log("playerId->" + playerInfo.playerId + "===plane distance->" + uiCanvas.planeDistance);
    //}

    //为外界准备的调用
    public void TakeDamage(int amount)
    {
        if (currentHp - amount > 0)
        {
            currentHp -= amount;
        }
        else
        {
            currentHp = 0;
            stateParams.Die();
        }
        healthSlider.TakeDamage(amount);
    }

    private void TakeDamageNotice(NotificationCenter.Notification info)
    {
        int id = (int)info.data;
        if(playerInfo.playerId == id)
        {
            //stateParams.onceActionType = Convert.ToInt16(CharacterStateMachine.OnceActionType.TakeDamage);
            //stateParams.triggerOnceAction = true;
            stateParams.TakeDamage();
        }
    }


    void TriggerSkill(NotificationCenter.Notification skillInfo)
    {
        if (stateParams.onceActionBegain) return;

        SkillLevelItem skill = (SkillLevelItem)skillInfo.data;
        //Debug.Log("Character里的skillid->" + skill.skillId);
        //stateParams.onceActionType = skill.skillId;
        //stateParams.triggerOnceAction = true;
        stateParams.ChangeOnceAction(skill.id);
    }

    void CharacterDie(NotificationCenter.Notification info)
    {
        int id = Convert.ToInt32(info.data);
        if (playerInfo.playerId == id)
            stateParams.Die();
    }

    void OnceActionChange(NotificationCenter.Notification info)
    {
        StateMachineParams param = (StateMachineParams)info.data;
        if (param.playerId == playerInfo.playerId)
        {
            if (stateParams.onceActionBegain) swordCollider.enabled = true;
            else swordCollider.enabled = false;//没有砍人时也要关闭
        }
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && stateParams.onceActionBegain)
        {
            GameObject enemy = other.gameObject;
            FootmanCharacter person = enemy.GetComponent<FootmanCharacter>();
            if (SkillModel.GetSkillById(playerInfo.careerId,onceActionType) != null)
            {
                SkillLevelItem skillLevel = SkillModel.GetSkillById(playerInfo.careerId, onceActionType);
                int damage = skillLevel.damage;
                person.TakeDamage(damage);

                swordCollider.enabled = false;
            }
        }
    }


}

