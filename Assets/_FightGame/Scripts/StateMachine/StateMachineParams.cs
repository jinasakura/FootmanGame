using UnityEngine;
public class StateMachineParams  {

    public bool isLive = true;
    public float speed = 0;
    public Vector3 moveVelocity = Vector3.zero;
    public int stayState = 0;
    public bool triggerOnceAction = false;
    public int onceActionType = 1;

    public bool canMove()
    {
        if (speed > CharacterInfo.stayOffset)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
