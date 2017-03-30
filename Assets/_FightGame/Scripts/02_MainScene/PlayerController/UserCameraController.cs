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


    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
        currentCameraRotationX = playerCamera.transform.localEulerAngles.x;
    }


    private void FixedUpdate()
    {
        bool currentMouseButtonDown = Input.GetMouseButton(0);
        if (currentMouseButtonDown && UltimateJoystick.GetVerticalAxis("Turn") != 0)
        {
            //float offsetX = Input.GetAxis("Mouse X");
            //PerformBodyRotation(offsetX * rotationSensitivity);
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

    ////如果直接把事件交给skill，会打破人物整个类的层次(需不需要这么死板？)
    //private void ReleaseSkill(NotificationCenter.Notification info)
    //{
    //    string skillName = (string)info.data;
    //    skillInfo = SkillModel.GetSkillLevelByName(skillName);
    //    if (skillInfo.CheckCondition(playerInfo.detail))
    //    {
    //        character.TriggerSkill(skillInfo.id);
    //        fight.TriggerSkill(skillInfo.mp);
    //    }
        
    //}

}


