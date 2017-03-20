using UnityEngine;
using System.Collections;


/// <summary>
/// 目前场景里只有我和AI
/// </summary>
public class RoleInputController : MonoBehaviour {

    protected PlayerInfo playerInfo;
    public SkillLevelItem skillInfo;

    protected FootmanStateMachine character;
    protected FootmanRoleFight fight;
    //protected FootmanSkill skill;

    protected Rigidbody rb;//其实照理来说人物转向也应该放到状态机里

    void Start () {
        init();
	}

    protected virtual void init()
    {
        character = GetComponent<FootmanStateMachine>();
        //skill = GetComponent<FootmanSkill>();
        fight = GetComponent<FootmanRoleFight>();

        playerInfo = GetComponent<PlayerInfo>();

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

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
