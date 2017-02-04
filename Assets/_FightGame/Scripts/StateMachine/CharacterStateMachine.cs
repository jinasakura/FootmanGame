using UnityEngine;
using System.Collections;

public class CharacterStateMachine :MonoBehaviour{

    private Animator animator;

    private StateMachineParams _stateParams;
    public StateMachineParams stateParams
    {
        get { return _stateParams; }
        set
        {
            _stateParams = value;
            Transition(_stateParams);
        }
    }

    private State currentState;

    private DieState die;
    private StayState stay;
    private OnceActionState onceAction;
    private MoveState move;

    public CharacterStateMachine(Animator ani)
    {
        animator = ani;

        die = new DieState(animator);
        stay = new StayState(animator);
        onceAction = new OnceActionState(animator);
        move = new MoveState(animator);
    }


    private bool inTransition = false;
    protected void Transition(StateMachineParams stateInfo)
    {
        if (!inTransition)
        {
            inTransition = true;
            if (stateInfo.isLive)
            {
                die.Exit();
                if (stateInfo.triggerOnceAction)
                {
                    onceAction.Enter();
                    stay.Enter();
                }
                else
                {
                    onceAction.Exit();
                    if (stateInfo.canMove())
                    {
                        move.Enter();
                        stay.Exit();
                    }
                    else
                    {
                        stay.Enter();
                        move.Exit();
                    }
                }
            }
            else
            {
                die.Enter();
            }
            object info = stateInfo;
            NotificationCenter.DefaultCenter.PostNotification(this, StateMachineEvent.HandleParamers, info);

            inTransition = false;
        }
    }

    void RecoverTrigger(NotificationCenter.Notification info)
    {
        StateMachineParams data = (StateMachineParams)info.data;
        Transition(data);
    }
}
