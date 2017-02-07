using UnityEngine;
public class StateMachineParams  {

    public bool isLive = true;
    public float speed = 0;
    public Vector3 moveVelocity = Vector3.zero;
    public int stayState = 0;
    public bool triggerOnceAction = false;
    //释放技能时，需要先利用这个变量把可能的移动转为静止
    public bool notMove = true;
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
