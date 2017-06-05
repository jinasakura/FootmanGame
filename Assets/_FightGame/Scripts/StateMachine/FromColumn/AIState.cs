using UnityEngine;
/// <summary>
/// AI状态基础类
/// </summary>
public class AIState : MonoBehaviour
{

    AIFSM _fsm;
    public AIFSM fsm
    {
        get
        {
            return _fsm;
        }
        set
        {
            _fsm = value;
        }
    }

    //跳出条件
    public StateConditionPair[] nextStates;

    [HideInInspector]
    public bool isStateFinish = false;

    private void Awake()
    {
        this.enabled = false;
        BaseAwake();
    }

    protected virtual void BaseAwake()
    {
    }

    public void BaseEnter()
    {
        enabled = true;
        isStateFinish = false;
        Enter();
    }

    public void BaseExecute()
    {
        if (IsGotoNextState())
        {
            return;
        }
        Execute();
    }

    public void BaseExit()
    {
        enabled = false;
        Exit();
    }


    protected virtual bool IsGotoNextState()
    {
        foreach (StateConditionPair scp in nextStates)
        {
            if (scp.IsReachCondition(fsm))
            {
                fsm.GotoState(scp.state);
                return true;
            }
        }
        return false;
    }

    protected virtual void Execute()
    {
    }

    protected virtual void Enter()
    {
    }

    protected virtual void Exit()
    {
    }

    /// <summary>
    /// 全局状态的执行，如果返回true则阻塞整个状态机
    /// </summary>
    /// <returns></returns>
    public virtual bool GlobalExecute()
    {
        return false;
    }


}