using UnityEngine;
using System;


/// <summary>
/// 判断状态机（废弃）
/// </summary>
public class FootmanStateMachine : StateMachine
{

    public static float STAY_OFFSET = 0.001f;//区分站立和走的临界数

    public enum OnceActionType { TakeDamage };

    public enum StayStateType { Idle, Victory, Upset, Defend };

    private StateMachineParams stateParams;

    private string _playerName;
    public string playerName { set; private get; }

    private SkillActionFire checkTouch;

    void Start()
    {
        stateParams = new StateMachineParams();
        checkTouch = GetComponent<SkillActionFire>();
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
            stateParams.stayState = Convert.ToInt16(StayStateType.Idle);
        }
        else
        {
            stateParams.moveVelocity = gameObject.transform.right * h + gameObject.transform.forward * v;
            stateParams.speed = Mathf.Max(tmpH, tmpV);
        }
        //Debug.Log("---状态机前---playerId->" + playerName + "===speed->" + stateParams.speed);

    }

    public override T GetState<T>()
    {
        T target = gameObject.GetComponent<T>();
        if (target == null)
        {
            target = gameObject.AddComponent<T>();
            gameObject.name = playerName;
        }
        return target;
    }

    public override void ChangeState<T>()
    {
        //if (currentState != null) currentState.enabled = false;
        if (stateParams.isLive)
        {
            if (stateParams.triggerOnceAction)
            {
                if (stateParams.onceActionType == 0)//受击
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

        //currentState.enabled = true;
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




}

