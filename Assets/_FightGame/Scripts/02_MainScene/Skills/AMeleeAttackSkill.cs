using UnityEngine;
using System.Collections;

public class AMeleeAttackSkill : RoleSkill
{

    //private GameObject ball;
    //private FireBallController controller;

    protected override void OnSkillFire(bool fire)
    {
        base.OnSkillFire(fire);

        if (skillActionStart)
        {
            //FireBall ball = new FireBall(new Vector3(0.5f,0.5f,0.5f),weaponCollider.gameObject.transform.position);
            //Vector3 direction = weaponCollider.gameObject.transform.position;
            //ball.AddVelocity(6.0f);
            GameObject ballPrefab = GetComponentInChildren<WizardWeaponController>().FireBall;
            GameObject ball = Instantiate(ballPrefab,gameObject.transform) as GameObject;

            FireBallController controller = ball.GetComponent<FireBallController>();
            controller.Fire(weaponCollider.gameObject.transform);
            controller.SetOwnerId(playerInfo.playerId);
            controller.SetRange(skillInfo.distance);
        }
    }

}
