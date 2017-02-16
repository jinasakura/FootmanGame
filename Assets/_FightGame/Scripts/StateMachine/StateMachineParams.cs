﻿using UnityEngine;
public class StateMachineParams : MonoBehaviour
{

    [SerializeField]
    private int _playerId;
    public int playerId
    {
        set { _playerId = value; }
        get { return _playerId; }
    }

    [SerializeField]
    private string _playerName;
    public string playerName
    {
        set { _playerName = value; }
        get { return _playerName; }
    }

    [SerializeField]
    private bool _isLive;
    public bool isLive
    {
        set
        {
            //Debug.Log("playerId->" + playerId + "-----isLive改变->" + _isLive);
            _isLive = value;
        }
        get
        {
            //Debug.Log("playerId->" + playerId + "-----isLive改变->" + _isLive);
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

    private int _stayState;
    public int stayState
    {
        set { _stayState = value; }
        get { return _stayState; }
    }

    private bool _triggerOnceAction;
    public bool triggerOnceAction
    {
        set { _triggerOnceAction = value; }
        get { return _triggerOnceAction; }
    }

    private int _onceActionType;
    public int onceActionType
    {
        set { _onceActionType = value; }
        get { return _onceActionType; }
    }

    public bool canMove()
    {
        if (speed > PlayerDetail.StayOffset)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
