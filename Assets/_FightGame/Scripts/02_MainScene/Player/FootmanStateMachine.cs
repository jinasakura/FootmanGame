using UnityEngine;
using System;


/// <summary>
/// 判断状态机（不能不用）
/// 为什么还是用状态继承自MonoBehaviour的？
/// 因为不用寻找状态类型和状态的对应关系了，获取状态的操作简单
/// 更重要的一点是，如果state不是MonoBehaviour,那么就只能封装那些变量，这样就不符合原来定的封装原则了
/// 比如，没法把移动操作包装到MoveState里。
/// </summary>
public class FootmanStateMachine : StateMachine
{

    public static float STAY_OFFSET = 0.001f;//区分站立和走的临界数

    public enum OnceActionType { TakeDamage };

    public enum StayStateType { Idle, Victory, Upset, Defend };

    private StateMachineParams stateParams;

    private string _playerName;
    public string playerName { set; private get; }
    //判断是否处于一个技能中
    private bool onSkill = false;

    private SkillActionFire checkTouch;

    void Start()
    {
        stateParams = new StateMachineParams();
        playerName = GetComponentInParent<PlayerInfo>().playerName;
        //checkTouch = GetComponent<SkillActionFire>();
        RoleSkillController skillController = GetComponentInParent<RoleSkillController>();
        if (skillController != null)
        {
            skillController.OnSkillTrigger += TriggerSkill;
        }
    }

    void FixedUpdate()
    {
        ChangeState<State>();
    }

    public void Move(float h, float v)
    {
        //if (onSkill) return;//技能和移动是互斥的,但是千万不能写这，否则就没法转化到其他状态了

        float tmpH = Mathf.Abs(h);
        float tmpV = Mathf.Abs(v);
        if ((tmpH <= STAY_OFFSET && tmpV <= STAY_OFFSET) || onSkill)
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
            gameObject.name = playerName+" Model";
        }
        return target;
    }

    public override void ChangeState<T>()
    {
        if (stateParams.isLive)
        {
            if (stateParams.triggerOnceAction)
            {
                if (stateParams.onceActionType == 0)//受击
                {
                    currentState = GetState<StruckState>();
                }
                else
                {
                    currentState = GetState<OnceActionState>();
                }
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
        if (onSkill) return;
        stateParams.onceActionType = skillId;
        stateParams.triggerOnceAction = true;
    }

    public void TakeDamageAction()
    {
        stateParams.onceActionType = (int)OnceActionType.TakeDamage;
        stateParams.triggerOnceAction = true;
    }

    //如果上一个技能动作还没结束不能开始下一个
    public void OnSkillState(bool state)
    {
        onSkill = state;
    }


}

