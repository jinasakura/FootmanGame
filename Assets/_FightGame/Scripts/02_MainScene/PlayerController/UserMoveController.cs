using UnityEngine;
using System.Collections;


/// <summary>
/// 职责：玩家（自己）移动控制
/// </summary>
public class UserMoveController : MonoBehaviour
{
    [SerializeField]
    protected float rotationSensitivity = 8f;//人物左右旋转系数

    protected FootmanStateMachine character;
    protected Rigidbody rb;

    void Start()
    {
        Init();
    }

    protected void Init()
    {
        character = GetComponentInChildren<FootmanStateMachine>();//在子类里控制状态机

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        MoveMode();
        TurnMode();
    }

    protected void MoveMode()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //float h = UltimateJoystick.GetHorizontalAxis("Move");
        //float v = UltimateJoystick.GetVerticalAxis("Move");
        character.Move(h, v);
    }

    protected void TurnMode()
    {
        bool currentMouseButtonDown = Input.GetMouseButton(0);
        if (currentMouseButtonDown && UltimateJoystick.GetVerticalAxis("Turn") != 0)
        {
            float offsetX = Input.GetAxis("Mouse X");
            PerformBodyRotation(offsetX * rotationSensitivity);
        }
    }

    //左右旋转rigidbody
    protected virtual void PerformBodyRotation(float offsetRot)
    {
        Vector3 offset = new Vector3(0f, offsetRot, 0f);
        //Debug.Log("fff"+offset);
        //四元数相乘代表什么？
        Quaternion q = rb.rotation * Quaternion.Euler(offset);
        rb.MoveRotation(q);
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
