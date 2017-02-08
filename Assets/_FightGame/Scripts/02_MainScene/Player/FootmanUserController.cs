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
    private float cameraRotationLimit = 90f;//最终的数值由外面决定
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
    }

    private float lastRotation = 0;
    private float offsetRotation = 0;
    private bool canRotate = false;

    private void FixedUpdate()
    {
        //float h = Input.GetAxis("Vertical");
        float h = UltimateJoystick.GetHorizontalAxis("Move");
        float v = UltimateJoystick.GetVerticalAxis("Move");
        character.Move(h, v);

        float cameraRot = UltimateJoystick.GetVerticalAxis("Look") * lookSensitivity;
        //PerformCameraRotation(cameraRot);

        float bodyRot = UltimateJoystick.GetHorizontalAxis("Look") * rotationSensitivity;
        offsetRotation = lastRotation - bodyRot;
        Debug.Log("上一次：" + lastRotation + "---这一次：" + bodyRot);
        lastRotation = bodyRot;
        if (offsetRotation != 0)
        {
            canRotate = true;
        }
        else canRotate = false;
        PerformBodyRotation(offsetRotation);

    }

    private Vector3 lastVecRotation = Vector3.zero;
    private Vector3 curVecRotation = Vector3.zero;
    //左右旋转rigidbody
    private void PerformBodyRotation(float offsetRot)
    {
        if (canRotate)
        {
            Debug.Log("帧旋转差：" + offsetRot);
            Vector3 offset = new Vector3(0f, offsetRot, 0f);
            curVecRotation += offset;
            Quaternion q = rb.rotation * Quaternion.Euler(curVecRotation);
            rb.MoveRotation(q);
            //if (curVecRotation != lastVecRotation)
            //{
            //    Quaternion q = rb.rotation * Quaternion.Euler(curVecRotation);
            //    rb.MoveRotation(q);
            //    lastVecRotation = curVecRotation;
            //}
        }


    }

    //上下旋转镜头
    private void PerformCameraRotation(float cameraRotationX)
    {
        if (PlayerCamera != null)
        {
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            PlayerCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
            //PlayerCamera.transform.Rotate(Vector3.up, currentCameraRotationX * Time.fixedDeltaTime);
        }
        else
        {
            Debug.LogWarning(
                "Warning: no Player camera found.");
        }
    }
}


