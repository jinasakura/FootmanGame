using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class FootmanCharacter : MonoBehaviour
{

    private CharacterStateMachine stateMachine;
    private bool onceActionBegain = false;

    private bool _isLive = true;
    public bool isLive
    {
        get { return _isLive; }
        set
        {
            _isLive = value;
            stateParams.isLive = _isLive;
        }
    }

    private int _stayState = 0;
    public int stayState
    {
        get { return _stayState; }
        set
        {
            _stayState = value;
            stateParams.stayState = _stayState;
        }
    }

    private int _onceActionType = 1;
    public int onceActionType
    {
        get { return _onceActionType; }
        set
        {
            _onceActionType = value;
            stateParams.onceActionType = _onceActionType;
        }
    }

    private float _speed = 0.0f;
    public float speed
    {
        get { return _speed; }
        set
        {
            _speed = value;
            stateParams.speed = _speed;
        }
    }

    //private bool _isUserControl;
    //public bool isUserControl { set; get; }

    //private int _playerId;
    //public int playerId { set; get; }

    //private string _playerName;
    //public string playerName { set; get; }
    private PlayerInfo _playerInfo;
    public PlayerInfo playerInfo { set; get; }

    private StateMachineParams stateParams;

    void Start()
    {
        if (LoginUserInfo.playerInfo.playerId == playerInfo.playerId)
        {
            NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.TriggerSkill);
            NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.CharacterLive);
            NotificationCenter.DefaultCenter.AddObserver(this, StateMachineEvent.OnceActionChange);
            
        }

        //stateMachine = GetComponent<CharacterStateMachine>();
        stateParams = GetComponent<StateMachineParams>();
        stateParams.playerId = playerInfo.playerId;
        stateParams.playerName = playerInfo.playerName;
        //Debug.Log("character init playerId->"+playerInfo.playerId+"------live->"+stateParams.isLive);
    }

    public void Move(float h, float v)
    {
        //时刻记得释放技能和移动是冲突的
        if (onceActionBegain)
        {
            stateParams.speed = 0;
            stateParams.moveVelocity = Vector3.zero;
            stateParams.stayState = Convert.ToInt16(CharacterStateMachine.StayStateType.Idle);
            //stateMachine.stateParams.notMove = false;
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


    void TriggerSkill(NotificationCenter.Notification skillInfo)
    {
        if (onceActionBegain) return;

        SkillItem skill = (SkillItem)skillInfo.data;
        //Debug.Log("Character里的skillid->" + skill.skillId);
        stateParams.onceActionType = skill.skillId;
        stateParams.triggerOnceAction = true;
        
    }

    void CharacterLive(NotificationCenter.Notification liveInfo)
    {
        //人物生死通过抛事件获得
        //重置生死后
        //UpdateAnimation();
    }

    void OnceActionChange(NotificationCenter.Notification info)
    {
        onceActionBegain = Convert.ToBoolean(info.data);
    }

}

