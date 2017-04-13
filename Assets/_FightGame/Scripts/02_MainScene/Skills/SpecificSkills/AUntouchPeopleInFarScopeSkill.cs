using UnityEngine;
using System.Collections;


/// <summary>
/// 远距离范围内非接触攻击多人
/// </summary>
public class AUntouchPeopleInFarScopeSkill : RoleSkill {

    private Transform rolePoint;

    void Start()
    {
        rolePoint = GetComponentInParent<PlayerInfo>().gameObject.transform;
    }

    protected override void OnSkillFire(bool fire)
    {
        base.OnSkillFire(fire);

        if (skillActionStart)
        {
            //GameObject ballPrefab = weaponController.FireBallLarge;
            //Vector3 des = rolePoint.position + weaponController.firePoint.localPosition;
            //GameObject ball = Instantiate(ballPrefab, weaponController.firePoint.position, rolePoint.rotation) as GameObject;

            //FireBallLargeController controller = ball.GetComponent<FireBallLargeController>();
            //controller.skillInfo = skillInfo;
            //controller.playerId = playerInfo.playerId;
            //controller.Fire(weaponController.firePoint);

            playerInfo.detail.DeductMp(skillInfo.mp);
        }
    }
}
