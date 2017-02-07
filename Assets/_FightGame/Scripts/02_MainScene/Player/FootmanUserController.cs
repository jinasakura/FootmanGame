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
        //不是很懂这里为什么要一个全局摄像机
        //if (Camera.main != null)
        //{
        //    mainCameraTrans = Camera.main.transform;
        //}
        //else
        //{
        //    Debug.LogWarning(
        //        "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
        //}

        character = GetComponent<FootmanCharacter>();
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        currentCameraRotationX = PlayerCamera.transform.localEulerAngles.x;
    }

    private void FixedUpdate()
    {
        //float h = Input.GetAxis("Vertical");
        float h = UltimateJoystick.GetHorizontalAxis("Move");
        float v = UltimateJoystick.GetVerticalAxis("Move");

        float yRot = UltimateJoystick.GetHorizontalAxis("Look") * rotationSensitivity;

        float xRot = UltimateJoystick.GetVerticalAxis("Look") * lookSensitivity;

        character.Move(h, v);
        PerformRotation(yRot, xRot);
    }


    private void PerformRotation(float bodyRotation, float cameraRotationX)
    {
        //左右旋转rigidbody
        Vector3 moveRotation = new Vector3(0f, bodyRotation, 0f);
        Quaternion q = rb.rotation * Quaternion.Euler(moveRotation);
        rb.MoveRotation(q);
        //上下旋转镜头
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


