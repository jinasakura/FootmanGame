using UnityEngine;
using System.Collections;


/// <summary>
/// 单发射物攻击
/// </summary>
public class AUniLancherSkill : RoleSkill {

    protected Transform firePoint;
    protected Transform rolePoint;

    public override void init()
    {
        base.init();
        string lanchPoint = SkillRef.FirePoint + playerInfo.playerId.ToString() + skillInfo.firePoint.ToString();
        firePoint = GameObject.Find(lanchPoint).transform;
        rolePoint = GetComponentInParent<PlayerInfo>().gameObject.transform;
    }

    protected override void OnSkillFire(bool fire)
    {
        base.OnSkillFire(fire);

        if (skillFireStart)
        {
            GameObject ballPrefab = LanchersController.GetLancherByName(skillInfo.lancherName);
            GameObject ball = Instantiate(ballPrefab, firePoint.position, rolePoint.rotation) as GameObject;
            //Debug.Log(firePoint.localPosition + "    " + firePoint.position);

            FireBallController controller = ball.GetComponent<FireBallController>();
            controller.skillInfo = skillInfo;
            controller.playerId = playerInfo.playerId;
            controller.Fire(rolePoint.forward,firePoint.position);

            //playerInfo.detail.DeductMp(skillInfo.mp);
        }
    }

}
