using UnityEngine;
using System.Collections;

public class RoleController : MonoBehaviour {

    protected PlayerInfo playerInfo;
    protected FootmanCharacter character;
    protected Rigidbody rb;
    protected Skill skill;

    void Start () {
        init();
	}

    protected virtual void init()
    {
        playerInfo = GetComponent<PlayerInfo>();
        character = GetComponent<FootmanCharacter>();

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        NotificationCenter.DefaultCenter.AddObserver(this, StateMachineEvent.OnceActionChange);

    }

    //左右旋转rigidbody
    protected virtual void PerformBodyRotation(float offsetRot)
    {
        Vector3 offset = new Vector3(0f, offsetRot, 0f);
        //四元数相乘代表什么？
        Quaternion q = rb.rotation * Quaternion.Euler(offset);
        rb.MoveRotation(q);
    }

    protected virtual void OnceActionChange()
    {
        character.OnSkillStateChange();
    }
}
