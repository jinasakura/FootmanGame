using UnityEngine;
using System;

public class StateMachineParams 
{

    public StateMachineParams()
    {
        isLive = true;
        speed = 0;
        moveVelocity = Vector3.zero;
        stayState = (int)SkillRef.StayStateType.Idle;
        triggerOnceAction = false;
        skillId = (int)SkillRef.SkillType.TakeDamage;
    }

    private bool _isLive;
    public bool isLive
    {
        set
        {
            _isLive = value;
        }
        get
        {
            return _isLive;
        }
    }

    private float _speed;
    public float speed
    {
        set
        {
            _speed = value;
        }
        get
        {
            return _speed;
        }
    }

    private Vector3 _moveVelocity;
    public Vector3 moveVelocity
    {
        set { _moveVelocity = value; }
        get { return _moveVelocity; }
    }

    private int _stayState;
    public int stayState
    {
        set { _stayState = value; }
        get { return _stayState; }
    }

    private bool _isSkill;
    public bool isSkill
    {
        set { _isSkill = value; }
        get { return _isSkill; }
    }
    
    private bool _triggerOnceAction;
    public bool triggerOnceAction
    {
        set { _triggerOnceAction = value; }
        get { return _triggerOnceAction; }
    }

    private int _skillId;
    public int skillId
    {
        set { _skillId = value; }
        get { return _skillId; }
    }

    public bool canMove()
    {
        if (speed > SkillRef.STAY_OFFSET)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int _loopTimes;
    public int loopTimes
    {
        set { _loopTimes = value; }
        get { return _loopTimes; }
    }

}
