using UnityEngine;
using System.Collections;

public class FootmanAIController : RoleInputController
{

    protected override void init()
    {
        base.init();

        character.playerName = playerInfo.playerName;
    }

    void FixedUpdate()
    {
        //float h = Random.Range(PlayerDetail.StayOffset, 0.5f);
        //float v = Random.Range(PlayerDetail.StayOffset, 0.5f);
        //character.Move(h, v);
    }

    private void Turn()
    {
        float offset = Random.Range(10, 90);
        PerformBodyRotation(offset);
        //Debug.Log("调用一次->" + offset);
    }

    ////左右旋转rigidbody
    //private void PerformBodyRotation(float offsetRot)
    //{
    //    Vector3 offset = new Vector3(0f, offsetRot, 0f);
    //    //四元数相乘代表什么？
    //    Quaternion q = rb.rotation * Quaternion.Euler(offset);
    //    rb.MoveRotation(q);
    //}
}
