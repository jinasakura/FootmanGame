using UnityEngine;
using System.Collections.Generic;

public class CharacterStateMachine : StateMachine {

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
            if (stateParams.triggerOnceAction)
            {
                currentState = GetState<OnceActionState>();
            }
            else if (stateParams.canMove())
            {
                currentState = GetState<MoveState>();
            }
            else
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
