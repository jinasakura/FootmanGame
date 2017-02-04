public class StateMachineParams  {

    public bool isLive = true;
    public float speed = 0;
    public int stayState = 0;
    public bool triggerOnceAction = false;
    public int onceActionType = 1;

    public bool canMove()
    {
        if (speed > FootmanCharacter.stayOffset)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
