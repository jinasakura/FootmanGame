using UnityEngine;
using System.Collections.Generic;
using System;

public class CharacterStateMachine : StateMachine
{

    public enum StayStateType { Idle, Victory, Upset, Defend };

    private StateMachineParams stateParams;

    private DieState die;
    private StayState stay;
    private OnceActionState onceAction;
    private MoveState move;

    void Start()
    {
        stateParams = GetComponent<StateMachineParams>();
        //Debug.Log("playerId->" + stateParams.playerId + "------状态机里live->" + stateParams.isLive);
    }

    void FixedUpdate()
    {
        ChangeState<State>();
    }

    public override T GetState<T>()
    {
        T target = GetComponent<T>();
        if (target == null)
        {
            target = gameObject.AddComponent<T>();
            gameObject.name = stateParams.playerName;
        }
        return target;
    }

    public override void ChangeState<T>()
    {
        if (stateParams.isLive)
        {
            if (stateParams.triggerOnceAction)// && stateParams.notMove
            {
                currentState = GetState<OnceActionState>();
            }
            else if (stateParams.canMove())
            {
                currentState = GetState<MoveState>();
            }
            else//技能需要先静止  if(!stateParams.canMove() || !stateParams.notMove)
            {
                currentState = GetState<StayState>();
            }
        }
        else
        {
            currentState = GetState<DieState>();
        }

        //Debug.Log(currentState.name);
        //if (stateParams.playerId == 0)
        //    Debug.Log("*****状态机中->" + stateParams.speed);
        NotificationCenter.DefaultCenter.PostNotification(this, StateMachineEvent.HandleParamers);

    }



}
