using UnityEngine;
using System.Collections;


/// <summary>
/// 用户输入控制
/// 原来把摄像机输入和屏幕输入分开了，现在觉得没必要，因为他们都是输入控制
/// </summary>
public class UserInputController : MonoBehaviour {

    [SerializeField]
    private float cameraUpperLimit = 40f;
    [SerializeField]
    private float cameraLowerLimit = -10f;
    [SerializeField]
    private float lookSensitivity = 4f;//镜头上下旋转的系数

    private float currentCameraRotationX = 0f;
    private Camera playerCamera;

    //************************************************************

    [SerializeField]
    protected float rotationSensitivity = 8f;//人物左右旋转系数

    protected PlayerStateMachine character;
    protected Rigidbody rb;

    void Start () {
        //摄像机输入
        playerCamera = Camera.main;
        currentCameraRotationX = playerCamera.transform.localEulerAngles.x;
        playerCamera.depth = 9999;

        //
        character = GetComponentInChildren<PlayerStateMachine>();//在子类里控制状态机
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        MoveMode();
        TurnMode();
        CameraMode();
    }

    private void MoveMode()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //float h = UltimateJoystick.GetHorizontalAxis("Move");
        //float v = UltimateJoystick.GetVerticalAxis("Move");
        character.Move(h, v);
    }

    private void TurnMode()
    {
        bool currentMouseButtonDown = Input.GetMouseButton(0);
        if (currentMouseButtonDown && UltimateJoystick.GetVerticalAxis("Turn") != 0)
        {
            float offsetX = Input.GetAxis("Mouse X");
            PerformBodyRotation(offsetX * rotationSensitivity);
        }
    }

    //左右旋转rigidbody
    private void PerformBodyRotation(float offsetRot)
    {
        Vector3 offset = new Vector3(0f, offsetRot, 0f);
        //Debug.Log("fff"+offset);
        //四元数相乘代表什么？
        Quaternion q = rb.rotation * Quaternion.Euler(offset);
        rb.MoveRotation(q);
    }

    private void CameraMode()
    {
        bool currentMouseButtonDown = Input.GetMouseButton(0);
        if (currentMouseButtonDown && UltimateJoystick.GetVerticalAxis("Turn") != 0)
        {
            //float offsetX = Input.GetAxis("Mouse X");
            //PerformBodyRotation(offsetX * rotationSensitivity);
            float offsetY = Input.GetAxis("Mouse Y");
            //Debug.Log(offsetY);
            PerformCameraRotation(offsetY * lookSensitivity);
            //UpDownCamera(offsetY);
        }
    }

    //上下旋转镜头
    private void PerformCameraRotation(float offsetY)
    {
        currentCameraRotationX -= offsetY;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, cameraLowerLimit, cameraUpperLimit);
        playerCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);

    }

}
