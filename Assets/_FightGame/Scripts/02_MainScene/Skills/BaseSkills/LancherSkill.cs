using UnityEngine;
using System.Collections;

public class LancherSkill : RoleSkill {

    protected Transform firePoint;
    protected Transform rolePoint;

    public override void init()
    {
        base.init();
        firePoint = weaponCollider.gameObject.transform.GetChild(0);
        rolePoint = GetComponentInParent<PlayerInfo>().gameObject.transform;
    }

    protected override void OnSkillFire(bool fire)
    {
        base.OnSkillFire(fire);

        if (skillActionStart)
        {
            GameObject ballPrefab = LanchersController.GetLancherByName(skillInfo.lancherName);
            GameObject ball = Instantiate(ballPrefab, firePoint.position, rolePoint.rotation) as GameObject;
            //Debug.Log(firePoint.localPosition + "    " + firePoint.position);

            FireBallController controller = ball.GetComponent<FireBallController>();
            controller.skillInfo = skillInfo;
            controller.playerId = playerInfo.playerId;
            controller.Fire(rolePoint.forward,firePoint.position);

            playerInfo.detail.DeductMp(skillInfo.mp);
        }
    }

}
