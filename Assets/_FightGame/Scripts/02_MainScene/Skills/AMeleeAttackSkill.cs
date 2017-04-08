using UnityEngine;
using System.Collections;

public class AMeleeAttackSkill : RoleSkill
{

    //private GameObject ball;
    //private FireBallController controller;
    private WizardWeaponController weaponController;
    private Transform rolePoint;

    void Start()
    {
        weaponController = GetComponentInChildren<WizardWeaponController>();
        rolePoint = GetComponentInParent<PlayerInfo>().gameObject.transform;
    }

    protected override void OnSkillFire(bool fire)
    {
        base.OnSkillFire(fire);

        if (skillActionStart)
        {
            GameObject ballPrefab = weaponController.FireBall;
            //Debug.Log(rolePoint.position + "      " + weaponController.firePoint.localPosition);
            Vector3 des = rolePoint.position + weaponController.firePoint.localPosition;
            //Debug.Log(des);
            GameObject ball = Instantiate(ballPrefab, des, weaponController.weaponPoint.rotation) as GameObject;

            FireBallController controller = ball.GetComponent<FireBallController>();
            controller.Fire(weaponController.firePoint);
            controller.SetOwnerId(playerInfo.playerId);
            controller.SetRange(skillInfo.distance);
        }
    }

}
