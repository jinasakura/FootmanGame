using UnityEngine;
using System.Collections;

/// <summary>
/// 将镜头的操作由原来FootmanCharacter放入这个类里
/// 因为Character里不应区分玩家与非玩家的操作
/// </summary>
[RequireComponent(typeof(FootmanCharacter))]
public class FootmanUserController : MonoBehaviour
{

    private FootmanCharacter character; // A reference to the ThirdPersonCharacter on the object
    //private Transform mainCameraTrans;                  // A reference to the main camera in the scenes transform
    private Rigidbody rb;
    private float currentCameraRotationX = 0f;

    [SerializeField]
    private float cameraUpperLimit = 90f;
    [SerializeField]
    private float cameraLowerLimit = 90f;
    [SerializeField]
    private float lookSensitivity = 3f;//镜头上下旋转的系数
    [SerializeField]
    private float rotationSensitivity = 4f;//人物左右旋转系数
    [SerializeField]
    private Camera PlayerCamera;

    //private float lastRotation = 0;
    //private float lastCamRotation = 0;

    //private float offsetRotation = 0;
    //private float offsetCamRotation = 0;
    //private bool canRotate = false;

    //private bool mouseDown = false;
    private Vector2 lastMousePosition;

    void Start()
    {
        character = GetComponent<FootmanCharacter>();
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        //currentCameraRotationX = lastMousePosition.y = PlayerCamera.transform.localEulerAngles.x;
        //mouseDown = Input.GetMouseButton(0);
    }

    private void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = UltimateJoystick.GetHorizontalAxis("Move");
        //float v = UltimateJoystick.GetVerticalAxis("Move");
        character.Move(h, v);

        bool currentMouseButtonDown = Input.GetMouseButton(0);
        if (currentMouseButtonDown)
        {
            // 处于长按状态
            Vector2 currentMousePosition = Input.mousePosition;

            float offSetX = currentMousePosition.x - lastMousePosition.x;
            if (offSetX > 365)//初始值太大，上一次结果为0，差值是一个很大的数，造成一开始镜头晃动
            {
                offSetX = 0;
            }
            PerformBodyRotation(offSetX * rotationSensitivity);
            //Debug.Log("镜头当前乘系数：" + currentMousePosition.x+"----上一次："+lastMousePosition.x);
            float offSetY = currentMousePosition.y - lastMousePosition.y;
            if (offSetY > cameraUpperLimit - cameraLowerLimit)//没想到更好的之前先这么做吧
            {
                offSetY = 0;
            }
            //Debug.Log(offSetX);
            PerformCameraRotation(offSetY * lookSensitivity);
            lastMousePosition = currentMousePosition;
        }
    }

    private Vector3 lastVecRotation = Vector3.zero;
    private Vector3 curVecRotation = Vector3.zero;
    //左右旋转rigidbody
    private void PerformBodyRotation(float offsetRot)
    {
        Vector3 offset = new Vector3(0f, offsetRot, 0f);
        //四元数相乘代表什么？
        Quaternion q = rb.rotation * Quaternion.Euler(offset);
        rb.MoveRotation(q);
    }

    //上下旋转镜头
    private void PerformCameraRotation(float offsetY)
    {
        Debug.Log("当前镜头：" + currentCameraRotationX + "---需要旋转：" + offsetY);
        currentCameraRotationX -= offsetY;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, cameraLowerLimit, cameraUpperLimit);
        //float curCamera = PlayerCamera.transform.localEulerAngles.x - offsetY;
        //float after = Mathf.Clamp(curCamera, cameraLowerLimit, cameraUpperLimit);
        //Debug.Log("旋转后："+currentCameraRotationX);
        PlayerCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
}


