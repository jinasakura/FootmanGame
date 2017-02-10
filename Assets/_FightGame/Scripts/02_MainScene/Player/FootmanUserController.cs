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

    void Start()
    {
        character = GetComponent<FootmanCharacter>();
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        currentCameraRotationX = PlayerCamera.transform.localEulerAngles.x;
        //mouseDown = Input.GetMouseButton(0);
    }

    private float lastRotation = 0;
    private float lastCamRotation = 0;

    private float offsetRotation = 0;
    private float offsetCamRotation = 0;
    private bool canRotate = false;


    //private bool mouseDown = false;

    private void FixedUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = UltimateJoystick.GetHorizontalAxis("Move");
        //float v = UltimateJoystick.GetVerticalAxis("Move");
        character.Move(h, v);

        //float cameraRot = UltimateJoystick.GetVerticalAxis("Look") * lookSensitivity;
        //PerformCameraRotation(cameraRot);

        if (Input.GetMouseButton(0))
        {
            //Debug.Log("开始旋转");
            //if (!mouseDown)
            //{

            //}
            offsetRotation = 0;
            offsetCamRotation = 0;
            canRotate = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("结束旋转");
            canRotate = false;
        }
        if (canRotate)
        {
            float bodyRot = Input.mousePosition.x * rotationSensitivity;
            //float bodyRot = UltimateJoystick.GetHorizontalAxis("Turn") * rotationSensitivity;
            offsetRotation = bodyRot - lastRotation;
            if (offsetRotation != 0)
            {
                //Debug.Log("当前值：" + bodyRot + "---上一次值：" + lastRotation + "---差值：" + offsetRotation);
                PerformBodyRotation(offsetRotation);
            }
            lastRotation = bodyRot;

            float cameraRot = Input.mousePosition.y * lookSensitivity;
            //float bodyRot = UltimateJoystick.GetHorizontalAxis("Turn") * rotationSensitivity;
            offsetCamRotation = cameraRot - lastCamRotation;
            if (offsetCamRotation != 0)
            {
                //Debug.Log("当前值：" + cameraRot + "---上一次值：" + lastCamRotation + "---差值：" + offsetCamRotation);
                //PerformCameraRotation(offsetCamRotation);
            }
            lastCamRotation = cameraRot;
        }


    }

    private Vector3 lastVecRotation = Vector3.zero;
    private Vector3 curVecRotation = Vector3.zero;
    //左右旋转rigidbody
    private void PerformBodyRotation(float offsetRot)
    {
        //Debug.Log("帧旋转差：" + offsetRot);
        //Vector3 curVec = rb.rotation.eulerAngles;
        Vector3 offset = new Vector3(0f, offsetRot, 0f);
        //curVec += offset;
        Quaternion q = rb.rotation * Quaternion.Euler(offset);
        rb.MoveRotation(q);
    }

    //上下旋转镜头
    private void PerformCameraRotation(float cameraRotationX)
    {
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, cameraLowerLimit, cameraUpperLimit);
        //Debug.Log("镜头：" + currentCameraRotationX);
        PlayerCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
}


