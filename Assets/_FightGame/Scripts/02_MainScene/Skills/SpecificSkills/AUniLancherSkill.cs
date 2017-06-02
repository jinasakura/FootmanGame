using UnityEngine;
using System.Collections;


/// <summary>
/// 单发射物攻击
/// </summary>
public class AUniLancherSkill : FarCombatSkill {

    protected override void OnSkillFire(int skillId,bool fire)
    {
        base.OnSkillFire(skillId,fire);

        if (skillFireStart)
        {
            GameObject ballPrefab = LanchersController.GetLancherByName(skillInfo.lancherName);
            GameObject ball = Instantiate(ballPrefab, firePoint.position, transform.rotation) as GameObject;
            //Debug.Log(firePoint.localPosition + "    " + firePoint.position);

            FireBallController controller = ball.GetComponent<FireBallController>();
            controller.skillInfo = skillInfo;
            controller.playerId = playerInfo.id;
            controller.Fire(transform.forward,firePoint.position);

        }
    }

}
