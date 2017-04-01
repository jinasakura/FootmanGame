using UnityEngine;
using System.Collections;


/// <summary>
/// 职责：控制角色移动和转向
/// </summary>
public class RoleMoveController : MonoBehaviour
{
    [SerializeField]
    protected float rotationSensitivity = 8f;//人物左右旋转系数

    protected RoleStateMachine character;
    protected Rigidbody rb;

    void Start()
    {
        Init();
    }

    void FixedUpdate()
    {
        MoveMode();
        TurnMode();
    }

    protected virtual void Init()
    {
        character = GetComponentInChildren<RoleStateMachine>();//在子类里控制状态机

        rb = GetComponentInChildren<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    protected virtual void MoveMode()
    {

    }

    protected virtual void TurnMode()
    {
        
    }


    //左右旋转rigidbody
    protected virtual void PerformBodyRotation(float offsetRot)
    {
        Vector3 offset = new Vector3(0f, offsetRot, 0f);
        //四元数相乘代表什么？
        Quaternion q = rb.rotation * Quaternion.Euler(offset);
        rb.MoveRotation(q);
    }


}
