using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class FootmanCharacter : MonoBehaviour
{

    private CharacterStateMachine stateMachine;


    private bool _isLive = true;
    public bool isLive
    {
        get { return _isLive; }
        set
        {
            _isLive = value;
            //改变状态机
            stateMachine.stateParams.isLive = _isLive;
        }
    }

    private int _stayState = 0;
    public int stayState
    {
        get { return _stayState; }
        set
        {
            _stayState = value;
            //改变状态机
            stateMachine.stateParams.stayState = _stayState;
        }
    }

    private int _onceActionType = 1;
    public int onceActionType
    {
        get { return _onceActionType; }
        set
        {
            _onceActionType = value;
            //改变状态机
            stateMachine.stateParams.onceActionType = _onceActionType;
        }
    }

    private float _speed = 0.0f;
    public float speed
    {
        get { return _speed; }
        set
        {
            _speed = value;
            //改变状态机
            stateMachine.stateParams.speed = _speed;
        }
    }

    private bool _triggerOnceAction = false;
    public bool triggerOnceAction
    {
        get { return _triggerOnceAction; }
        set
        {
            _triggerOnceAction = value;
            //改变状态机
            stateMachine.stateParams.triggerOnceAction = _triggerOnceAction;
        }
    }


    void Awake()
    {
        stateMachine = GetComponent<CharacterStateMachine>();

        NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.TriggerSkill);
        NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.CharacterLive);

    }

    public void Move(float h, float v)
    {
        float tmpH = Mathf.Abs(h);
        float tmpV = Mathf.Abs(v);
        if (tmpH <= CharacterInfo.stayOffset && tmpV <= CharacterInfo.stayOffset)
        {
            stateMachine.stateParams.speed = 0;
        }
        else
        {
            stateMachine.stateParams.moveVelocity = transform.right * h + transform.forward * v;
            stateMachine.stateParams.speed = Mathf.Max(tmpH, tmpV);
        }

    }


    void TriggerSkill(NotificationCenter.Notification skillInfo)
    {
        SkillItem skill = (SkillItem)skillInfo.data;
        stateMachine.stateParams.onceActionType = skill.skillId;
        stateMachine.stateParams.triggerOnceAction = true;
    }

    void CharacterLive(NotificationCenter.Notification liveInfo)
    {
        //人物生死通过抛事件获得
        //重置生死后
        //UpdateAnimation();
    }


}

