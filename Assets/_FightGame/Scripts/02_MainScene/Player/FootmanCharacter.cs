using UnityEngine;
using System;


/// <summary>
/// 判断状态机
/// </summary>
public class FootmanCharacter : MonoBehaviour
{

    public static float STAY_OFFSET = 0.001f;//区分站立和走的临界数


    private StateMachineParams stateParams;

    void Start()
    {
        stateParams = GetComponent<StateMachineParams>();
    }

    public void Move(float h, float v)
    {
        float tmpH = Mathf.Abs(h);
        float tmpV = Mathf.Abs(v);
        if (tmpH <= STAY_OFFSET && tmpV <= STAY_OFFSET)
        {
            stateParams.speed = 0;
            stateParams.moveVelocity = Vector3.zero;
            stateParams.stayState = Convert.ToInt16(CharacterStateMachine.StayStateType.Idle);
        }
        else
        {
            stateParams.moveVelocity = transform.right * h + transform.forward * v;
            stateParams.speed = Mathf.Max(tmpH, tmpV);
        }
        //Debug.Log("---状态机前---playerId->" + playerInfo.playerId + "===speed->" + stateParams.speed);

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






}

