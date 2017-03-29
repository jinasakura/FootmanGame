using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// 新版判断状态机
/// </summary>
public class RoleStateMachine : MonoBehaviour
{

    public static float STAY_OFFSET = 0.001f;//区分站立和走的临界数

    public enum OnceActionType { TakeDamage };

    //public enum StayStateType { Idle, Victory, Upset, Defend };
    public enum StateType { Stay,Move,NomalOnceAction,Die,Stuck };

    private StateType _currentStateType;
    public StateType currentStateType { private set; get; }

    private bool _inTransition = false;
    private StateMachineParams stateParams;
    private Animator animator;
    private Rigidbody rb;
    private Dictionary<StateType, NewState> stateDict;

    void Start()
    {
        stateParams = new StateMachineParams();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        initStates();
    }

    private void initStates()
    {
        stateDict = new Dictionary<StateType, NewState>();
        stateDict[StateType.Stay] = new StayState(GetComponent<Animator>());
    }

    void FixedUpdate()
    {
        ChangeState<State>();
    }

    public void Move(float h, float v)
    {
        float tmpH = Mathf.Abs(h);
        float tmpV = Mathf.Abs(v);
        if (tmpH <= STAY_OFFSET && tmpV <= STAY_OFFSET)
        {
            stateParams.speed = 0;
            stateParams.moveVelocity = Vector3.zero;
            //stateParams.stayState = Convert.ToInt16(StayStateType.Idle);
        }
        else
        {
            stateParams.moveVelocity = gameObject.transform.right * h + gameObject.transform.forward * v;
            stateParams.speed = Mathf.Max(tmpH, tmpV);
        }
        //Debug.Log("---状态机前---playerId->" + playerName + "===speed->" + stateParams.speed);

    }

    

    public NewState GetState()
    {
        NewState target = stateDict[currentStateType];
        if (target == null)
        {
            switch (currentStateType)
            {
                case StateType.Stay:
                    target= new StayState(GetComponent<Animator>());
                    break;
                case StateType.Move:
                    target = new MoveState(GetComponent<Animator>(),GetComponent<Rigidbody>());
                    break;
                case StateType.Die:
                    break;
                case StateType.NomalOnceAction:
                    break;
                case StateType.Stuck:
                    break;
            }
        }
        return target;
    }

    public void ChangeState<T>()
    {
        //if (currentState != null) currentState.enabled = false;
        if (stateParams.isLive)
        {
            if (stateParams.triggerOnceAction)
            {
                if (stateParams.onceActionType == (int)OnceActionType.TakeDamage)//受击
                {
                    //currentState = GetState<StruckState>();
                }
                else
                {
                    //currentState = GetState<OnceActionState>();
                }
            }
            else if (stateParams.canMove())
            {
                //currentState = GetState<MoveState>();
            }
            else
            {
                //currentState = GetState<StayState>();
            }
        }
        else
        {
            //currentState = GetState<DieState>();
        }

        //currentState.HandleParamers(stateParams);
    }

    public void Die()
    {
        stateParams.isLive = false;
    }

    public void Live()
    {
        stateParams.isLive = true;
    }

    public void TriggerSkill(int skillId)
    {
        stateParams.onceActionType = skillId;
        stateParams.triggerOnceAction = true;
    }

    public void TakeDamageAction()
    {
        stateParams.onceActionType = (int)OnceActionType.TakeDamage;
        stateParams.triggerOnceAction = true;
    }

    private void Transition(StateType value)
    {
        if (currentStateType == value || _inTransition) return;

        _inTransition = true;

        //if (_currentState != null) _currentState.Exit();

        //_currentState = value;

        //if (_currentState != null) _currentState.Enter();

        _inTransition = false;
    }


}

