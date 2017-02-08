using UnityEngine;
using System.Collections.Generic;
using System;

public class CharacterStateMachine : StateMachine {

    public enum StayStateType { Idle,Victory,Upset,Defend };

    private StateMachineParams _stateParams;
    public StateMachineParams stateParams
    {
        get { return _stateParams; }
        set
        {
            _stateParams = value;
        }
    }

    private DieState die;
    private StayState stay;
    private OnceActionState onceAction;
    private MoveState move;

    void Awake()
    {
        if (stateParams == null)
        {
            stateParams = new StateMachineParams();
        }
    }

    void FixedUpdate()
    {
        ChangeState<State>();
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
        NotificationCenter.DefaultCenter.PostNotification(this, StateMachineEvent.HandleParamers, stateParams);
    }



}
