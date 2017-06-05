using UnityEngine;

public enum AIConditionType
{
    IsDistanceGreaterThan,
    IsDistanceLessThan,
    IsHpGreaterThan,
    IsHpLessThan,
    IsTimeGreaterThan,
    IsReachDestination,
    IsStateFinish
}

[System.Serializable]
public class StateCondition
{
    public AIConditionType type;
    public float value;
    public GameObject valueObj;

    public bool IsReachCondition(AIFSM fsm)
    {

        //bool isReach = false;
        switch (type)
        {
            case AIConditionType.IsDistanceGreaterThan:
                {
                    //TODO 判断如果距离大于value就返回true
                }
                break;
            case AIConditionType.IsDistanceLessThan:
                {
                    //TODO 判断如果距离小于value就返回true
                }
                break;
            case AIConditionType.IsHpGreaterThan:
                {
                    //TODO 判断如果血量大于value就返回true
                }
                break;
            case AIConditionType.IsHpLessThan:
                {
                    //TODO 判断如果血量小于value就返回true
                }
                break;
            case AIConditionType.IsTimeGreaterThan:
                {
                    //TODO 判断如果运行时间超过value就返回true
                }
                break;
            case AIConditionType.IsReachDestination:
                {
                    //TODO 判断如果到达目标valueObj返回true
                }
                break;
            case AIConditionType.IsStateFinish:
                {
                    //TODO 判断如果状态结束完成就返回true
                }
                break;
        }

        return false;
    }
}