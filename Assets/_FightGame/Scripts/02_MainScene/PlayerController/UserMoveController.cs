using UnityEngine;
using System.Collections;


/// <summary>
/// 职责：玩家（自己）移动控制
/// </summary>
public class UserMoveController : RoleMoveController
{

    protected override void MoveMode()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //float h = UltimateJoystick.GetHorizontalAxis("Move");
        //float v = UltimateJoystick.GetVerticalAxis("Move");
        character.Move(h, v);
    }

    protected override void TurnMode()
    {
        bool currentMouseButtonDown = Input.GetMouseButton(0);
        if (currentMouseButtonDown && UltimateJoystick.GetVerticalAxis("Turn") != 0)
        {
            float offsetX = Input.GetAxis("Mouse X");
            PerformBodyRotation(offsetX * rotationSensitivity);
        }
    }

    //void FixedUpdate()
    //{
    //    float h = Input.GetAxis("Horizontal");
    //    float v = Input.GetAxis("Vertical");
    //    //float h = UltimateJoystick.GetHorizontalAxis("Move");
    //    //float v = UltimateJoystick.GetVerticalAxis("Move");
    //    //if (!fight.skillBegain)
    //    //{
    //        character.Move(h, v);
    //    //}
    //}
}
