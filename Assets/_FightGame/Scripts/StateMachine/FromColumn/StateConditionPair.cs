[System.Serializable]
public class StateConditionPair
{
    private bool avaliable = true;//是否启用
    public AIState state;//下一个状态
    public StateCondition[] conditions;//并列条件，必须全部完成

    //是否达到离开的条件
    public bool IsReachCondition(AIFSM fsm)
    {
        if (!avaliable)
        {
            return false;
        }
        if (conditions.Length == 0)
        {
            return false;
        }

        foreach (StateCondition condition in conditions)
        {
            if (!condition.IsReachCondition(fsm))
            {
                return false;
            }
        }
        return true;

    }


}