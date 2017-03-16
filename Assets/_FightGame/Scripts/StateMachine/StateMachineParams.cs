using UnityEngine;
using System;
//不是很懂为什么set、get不能写成简写的形式
public class StateMachineParams : MonoBehaviour
{

    //[SerializeField]
    //private int _playerId;
    //public int playerId
    //{
    //    set { _playerId = value; }
    //    get { return _playerId; }
    //}

    //[SerializeField]
    //private string _playerName;
    //public string playerName
    //{
    //    set { _playerName = value; }
    //    get { return _playerName; }
    //}

    [SerializeField]
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

    [SerializeField]
    private float _speed;
    public float speed
    {
        set
        {
            _speed = value;
            //if (playerId == 0)
            //    Debug.Log("playerId->" + playerId + "-----speed  set->" + _speed);
        }
        get
        {
            //if (playerId == 0 && _speed > 0.2)
            //    Debug.Log("playerId->" + playerId + "-----speed  get->" + _speed);
            return _speed;
        }
    }

    private Vector3 _moveVelocity;
    public Vector3 moveVelocity
    {
        set { _moveVelocity = value; }
        get { return _moveVelocity; }
    }

    [SerializeField]
    private int _stayState;
    public int stayState
    {
        set { _stayState = value; }
        get { return _stayState; }
    }

    //[SerializeField]
    //private bool _onceActionBegain;
    //public bool onceActionBegain
    //{
    //    set { _onceActionBegain = value; }
    //    get { return _onceActionBegain; }
    //}
    
    private bool _triggerOnceAction;
    public bool triggerOnceAction
    {
        set { _triggerOnceAction = value; }
        get { return _triggerOnceAction; }
    }

    [SerializeField]
    private int _onceActionType;
    public int onceActionType
    {
        set { _onceActionType = value; }
        get { return _onceActionType; }
    }

    public bool canMove()
    {
        if (speed > FootmanCharacter.STAY_OFFSET)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //public void Idle()
    //{
    //    speed = 0;
    //    moveVelocity = Vector3.zero;
    //    stayState = Convert.ToInt16(CharacterStateMachine.StayStateType.Idle);
    //}

    //public void ChangeOnceAction(int type)
    //{
    //    onceActionType = type;
    //    triggerOnceAction = true;
    //}

    //public void TakeDamage()
    //{
    //    onceActionType = Convert.ToInt16(CharacterStateMachine.OnceActionType.TakeDamage);
    //    triggerOnceAction = true;
    //}

    //public void Die()
    //{
    //    isLive = false;
    //}

    //public void Live()
    //{
    //    isLive = true;
    //}
}
