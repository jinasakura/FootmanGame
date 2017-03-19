using UnityEngine;
using System.Collections;

/// <summary>
/// 将镜头的操作由原来FootmanCharacter放入这个类里
/// 因为Character里不应区分玩家与非玩家的操作
/// 输入、少量执行
/// </summary>
public class FootmanRoleController : RoleInputController
{

    private float currentCameraRotationX = 0f;

    [SerializeField]
    private float cameraUpperLimit = 40f;
    [SerializeField]
    private float cameraLowerLimit = -10f;
    [SerializeField]
    private float lookSensitivity = 4f;//镜头上下旋转的系数
    [SerializeField]
    private float rotationSensitivity = 8f;//人物左右旋转系数


    private Camera playerCamera;

    protected override void init()
    {
        base.init();
        
        playerCamera = gameObject.GetComponentInChildren<Camera>();
        currentCameraRotationX = playerCamera.transform.localEulerAngles.x;
        
        character.playerName = playerInfo.playerName;

        //只要保证了是自己才能接收消息就行
        if (playerInfo.playerId == LoginUserInfo.playerInfo.playerId)
        {
            NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.ReleaseSkill);
        }
    }


    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //float h = UltimateJoystick.GetHorizontalAxis("Move");
        //float v = UltimateJoystick.GetVerticalAxis("Move");
        if (!skill.skillBegain)
        {
            character.Move(h, v);
        }
        
        //Debug.Log("h->"+h);
        bool currentMouseButtonDown = Input.GetMouseButton(0);
        if (currentMouseButtonDown && UltimateJoystick.GetVerticalAxis("Turn") != 0)
        {
            float offsetX = Input.GetAxis("Mouse X");
            //Debug.Log("offsetX->" + offsetX);
            PerformBodyRotation(offsetX * rotationSensitivity);
            float offsetY = Input.GetAxis("Mouse Y");
            PerformCameraRotation(offsetY * lookSensitivity);
        }

    }

    //上下旋转镜头
    private void PerformCameraRotation(float offsetY)
    {
        currentCameraRotationX -= offsetY;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, cameraLowerLimit, cameraUpperLimit);
        playerCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    //如果直接把事件交给skill，会打破人物整个类的层次
    private void ReleaseSkill(NotificationCenter.Notification info)
    {
        string skillName = (string)info.data;
        skill.OnReleaseSkill(skillName);
    }

}


