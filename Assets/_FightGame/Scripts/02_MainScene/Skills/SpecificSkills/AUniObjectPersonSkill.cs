using UnityEngine;
using System.Collections;


/// <summary>
/// 单发射物攻击单人
/// </summary>
public class AUniObjectPersonSkill : LancherSkill
{


    void Start()
    {
        firePoint = weaponCollider.gameObject.transform.GetChild(0);
    }

    protected override void OnSkillFire(bool fire)
    {
        base.OnSkillFire(fire);

        if (skillActionStart)
        {
            //GameObject ballPrefab = LanchersController.GetLancherByName(skillInfo.lancherName);
            //GameObject ball = Instantiate(ballPrefab, weaponController.firePoint.position, rolePoint.rotation) as GameObject;

            //FireBallSmallController controller = ball.GetComponent<FireBallSmallController>();
            //controller.skillInfo = skillInfo;
            //controller.playerId = playerInfo.playerId;
            //controller.Fire(weaponController.firePoint);

            playerInfo.detail.DeductMp(skillInfo.mp);
        }
    }

}
