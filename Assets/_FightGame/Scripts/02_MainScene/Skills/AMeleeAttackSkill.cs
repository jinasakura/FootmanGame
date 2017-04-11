using UnityEngine;
using System.Collections;

public class AMeleeAttackSkill : RoleSkill
{

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
            Vector3 des = rolePoint.position + weaponController.firePoint.localPosition;
            GameObject ball = Instantiate(ballPrefab, weaponController.firePoint.position, rolePoint.rotation) as GameObject;

            FireBallController controller = ball.GetComponent<FireBallController>();
            controller.Fire(weaponController.firePoint);
            controller.SetOwnerId(playerInfo.playerId);
            controller.skillInfo = skillInfo;

            playerInfo.detail.DeductMp(skillInfo.mp);
        }
    }

}
