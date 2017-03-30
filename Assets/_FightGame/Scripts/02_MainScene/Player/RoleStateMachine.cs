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

    private NewState _currentState;
    public NewState currentState {
        get { return _currentState; }
        set { Transition(value); }
    }

    private bool _inTransition = false;
    private StateMachineParams stateParams;
    private Animator animator;
    private Rigidbody rb;
    private Dictionary<StateType, NewState> stateDict;

    void Start()
    {
        stateParams = new StateMachineParams();
        stateDict = new Dictionary<StateType, NewState>();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
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

    void FixedUpdate()
    {
        ChangeState();
    }

    public NewState GetState()
    {
        NewState target = stateDict[currentStateType];
        if (target == null)
        {
            Animator ani = GetComponent<Animator>();
            switch (currentStateType)
            {
                case StateType.Stay:
                    target= new StayState(ani);
                    break;
                case StateType.Move:
                    Rigidbody rbd = GetComponent<Rigidbody>();
                    target = new MoveState(ani, rbd);
                    break;
                case StateType.Die:
                    target = new DieState(ani);
                    break;
                case StateType.NomalOnceAction:
                    target = new OnceActionState(ani);
                    break;
                case StateType.Stuck:
                    target = new StruckState(ani);
                    break;
            }
            stateDict[currentStateType] = target;
        }
        return target;
    }

    public void ChangeState()
    {
        if (stateParams.isLive)
        {
            if (stateParams.triggerOnceAction)
            {
                if (stateParams.onceActionType == (int)OnceActionType.TakeDamage)//受击
                {
                    currentStateType = StateType.Stuck;
                }
                else
                {
                    currentStateType = StateType.NomalOnceAction;
                }
            }
            else if (stateParams.canMove())
            {
                currentStateType = StateType.Move;
            }
            else
            {
                currentStateType = StateType.Stay;
            }
        }
        else
        {
            currentStateType = StateType.Die;
        }
        currentState = GetState();
        currentState.HandleParamers(stateParams);
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

    private void Transition(NewState value)
    {
        if (_currentState == value || _inTransition) return;

        _inTransition = true;

        if (_currentState != null) _currentState.Exit();

        _currentState = value;

        if (_currentState != null) _currentState.Enter();

        _inTransition = false;
    }


}

