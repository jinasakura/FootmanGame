using UnityEngine;
using System.Collections;

/// <summary>
/// 玩家自己
/// 职责：只控制用户上下旋转镜头
/// </summary>
public class UserCameraController : MonoBehaviour
{

    private float currentCameraRotationX = 0f;

    [SerializeField]
    private float cameraUpperLimit = 40f;
    [SerializeField]
    private float cameraLowerLimit = -10f;
    [SerializeField]
    private float lookSensitivity = 4f;//镜头上下旋转的系数

    //[SerializeField]
    //private float zLower = -3.37f;
    //[SerializeField]
    //private float zUpper = -1.63f;
    //private float currentZ;
    //[SerializeField]
    //private float zSensitivity = 0.5f;

    //[SerializeField]
    //private float yLower = 1.94f;
    //[SerializeField]
    //private float yUpper = 2.97f;
    //private float currentY;
    //[SerializeField]
    //private float ySensitivity = 0.5f;


    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
        currentCameraRotationX = playerCamera.transform.localEulerAngles.x;
        playerCamera.depth = int.MaxValue - 1;
        //currentZ = playerCamera.transform.localPosition.z;
        //currentY = playerCamera.transform.localPosition.y;
    }


    private void FixedUpdate()
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

    //private void UpDownCamera(float offset)
    //{
    //    currentZ -= offset*zSensitivity;
    //    currentZ= Mathf.Clamp(currentZ, zLower, zUpper);
    //    currentY -= offset*ySensitivity;
    //    currentY = Mathf.Clamp(currentY, yLower, yUpper);
    //    playerCamera.transform.position = new Vector3(0f, currentY, currentZ);
    //}


}


