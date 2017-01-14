using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoleStateMachine : MonoBehaviour {

    private class RoleStateData
    {
        public RoleState[] from;
        public RoleState[] to;
        private static bool StateInArray(RoleState value, RoleState[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == value)
                    return true;
            }
            return false;
        }
        public bool CouldFrom(RoleState state)
        {
            return RoleStateData.StateInArray(state, from);
        }
        public bool CouldTo(RoleState state)
        {
            return RoleStateData.StateInArray(state, to);
        }
    }

    public enum RoleState
    {
        Stay = 0, Move, Die, OnceAction
    };
    [SerializeField]
    private RoleState currentState = RoleState.Stay;

    private Dictionary<RoleState, RoleStateData> roleStateSetting;
    private void SetRoleStateSetting(RoleState settingState, RoleState[] fromStates, RoleState[] toStates)
    {
        if (roleStateSetting == null)
        {
            roleStateSetting = new Dictionary<RoleState, RoleStateData>();
        }
        RoleStateData settingData = new RoleStateData();
        settingData.from = fromStates;
        settingData.to = fromStates;
        roleStateSetting.Add(settingState, settingData);
    }

    void Start()
    {
        RoleState[] allState = new RoleState[] {RoleState.Die, RoleState.Move, RoleState.OnceAction, RoleState.Stay};
        SetRoleStateSetting(RoleState.Stay,
            new RoleState[] { RoleState.Die }, 
            new RoleState[] { RoleState.Move });
        SetRoleStateSetting(RoleState.Move,
            new RoleState[] { RoleState.Stay, RoleState.OnceAction },
            new RoleState[] { RoleState.Stay });
        SetRoleStateSetting(RoleState.OnceAction,
            allState,
            new RoleState[] { RoleState.Move });
        SetRoleStateSetting(RoleState.Die,
            allState,
            new RoleState[] { RoleState.Stay });
    }

    public bool TransState(RoleState toState)
    {
        RoleStateData currentData = roleStateSetting[currentState];
        bool couldTrans = currentData.CouldTo(toState);
        if (couldTrans)
        {
            couldTrans = RoleStateTransCondition(toState);
            if (couldTrans)
            {
                currentState = toState;
                OnStateTransd();
            }
        }
        return couldTrans;
    }

    private bool RoleStateTransCondition(RoleState toState)
    {
        // TODO: 判断角色属性是否可以切换
        return false;
    }

    private void OnStateTransd()
    {
        switch (currentState)
        {
            case RoleState.Stay:
                // TODO: 状态切换设置
                break;
            case RoleState.Die:
                // TODO: 状态切换设置
                break;
            case RoleState.Move:
                // TODO: 状态切换设置
                break;
            case RoleState.OnceAction:
                // TODO: 状态切换设置
                break;
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
