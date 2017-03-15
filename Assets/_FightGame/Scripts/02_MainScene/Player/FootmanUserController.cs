using UnityEngine;
using System.Collections;

/// <summary>
/// 将镜头的操作由原来FootmanCharacter放入这个类里
/// 因为Character里不应区分玩家与非玩家的操作
/// </summary>
public class FootmanUserController : RoleController
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
        currentCameraRotationX = playerCamera.transform.localEulerAngles.x;

        NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.ReleaseSkill);
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //float h = UltimateJoystick.GetHorizontalAxis("Move");
        //float v = UltimateJoystick.GetVerticalAxis("Move");
        character.Move(h, v);
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


    ////左右旋转rigidbody
    //private void PerformBodyRotation(float offsetRot)
    //{
    //    Vector3 offset = new Vector3(0f, offsetRot, 0f);
    //    //四元数相乘代表什么？
    //    Quaternion q = rb.rotation * Quaternion.Euler(offset);
    //    rb.MoveRotation(q);

    //}

    //上下旋转镜头
    private void PerformCameraRotation(float offsetY)
    {
        currentCameraRotationX -= offsetY;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, cameraLowerLimit, cameraUpperLimit);
        playerCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);

    }

    private void ReleaseSkill(NotificationCenter.Notification info)
    {
        string skillName = (string)info.data;
        SkillLevelItem skillInfo = SkillModel.GetSkillLevelByName(skillName);

        skill = GetComponent<Skill>();
        if (skill != null)
        {
            skill = gameObject.AddComponent<Skill>();
            gameObject.name = playerInfo.playerName;
            skill.skillInfo = skillInfo;
            skill.Caster(LoginUserInfo.playerInfo.playerId);
            if (skill.CheckCondition(LoginUserInfo.playerInfo.detail)) { skill.Trigger(); }
            
        }
    }
}


