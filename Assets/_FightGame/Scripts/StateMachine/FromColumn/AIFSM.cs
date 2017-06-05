using UnityEngine;
using System.Collections.Generic;

public class AIFSM : MonoBehaviour {

    public AIState currentState;

    //全局状态
    public AIState[] globalStates;

    //进入第一个状态的条件
    public StateConditionPair[] nextStates;
    //非全局状态，用于初始化
    public List<AIState> allStates = new List<AIState>();

    public void GotoState(AIState state)
    {


        if (currentState != null)
        {
            currentState.BaseExit();
        }

        if (state == null)
        {
            currentState = null;
            return;
        }

        currentState = state;
        currentState.fsm = this;
        currentState.BaseEnter();

    }

    //运行所有全局状态，如果返回false说明没有全局状态阻塞状态机
    bool ExecuteGlobalState(out AIState executeState)
    {
        foreach (AIState state in globalStates)
        {
            if (state == null)
            {
                continue;
            }
            state.fsm = this;

            if (state.GlobalExecute())
            {
                state.enabled = true;
                executeState = state;
                return true;
            }
            else
            {
                state.enabled = false;
            }
        }
        executeState = null;
        return false;
    }

    //自动进入第一个状态
    public bool IsGotoNextState()
    {
        foreach (StateConditionPair scp in nextStates)
        {
            if (scp.IsReachCondition(this))
            {
                GotoState(scp.state);
                return true;
            }
        }
        return false;
    }


    void Update()
    {

        AIState executeState = null;
        //运行全局状态，看是否有全局状态阻塞状态机，如果没有就再继续运行后面的状态。
        if (ExecuteGlobalState(out executeState))
        {

            currentState = null;
            return;
        }

        //如果当前状态为空，自动找一个状态进入
        if (currentState == null)
        {
            IsGotoNextState();
        }
        //当前状态不为空，就执行
        if (currentState)
        {
            currentState.BaseExecute();
        }
    }
}
