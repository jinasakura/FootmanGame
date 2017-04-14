using UnityEngine;
using System.Collections;


/// <summary>
/// 在左边或右边复制出另一个自己
/// </summary>
public class ACloneSelfSkill : RoleSkill
{

    private GameObject roleBase;

    protected override void OnSkillFire(bool fire)
    {
        base.OnSkillFire(fire);

        if (skillFireStart)
        {
            Vector3 position = transform.position;
            position.y += 0.5f;
            Ray ray = new Ray(position, transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * skillInfo.releaseDistance, Color.cyan, 10);

            RaycastHit hitInfo;
            if (!Physics.Raycast(ray, out hitInfo, skillInfo.releaseDistance))
            {
                //Debug.Log(hitInfo.collider.gameObject.name);
                //Debug.Log(hitInfo.distance);
                Vector3 newPlayerPosition = transform.position;
                newPlayerPosition.z += Mathf.Floor(skillInfo.releaseDistance / 2);

                GameObject model = PlayerModel.GetModelByName(playerInfo.modelName);
                roleBase = Instantiate(PlayerModel.roleBasePrefab, newPlayerPosition, transform.rotation) as GameObject;
                GameObject playerModel = Instantiate(model, newPlayerPosition, transform.rotation, roleBase.transform) as GameObject;
                PlayerInfo pInfo = roleBase.GetComponent<PlayerInfo>();
                pInfo.playerId = playerInfo.playerId;
                playerInfo.playerName = "Player channeling";
                pInfo.modelName = playerInfo.modelName;
                pInfo.detail = new PlayerDetailInfo();
                pInfo.detail.careerId = LoginUserInfo.careerId;
                pInfo.detail.level = LoginUserInfo.level;
                CareerItem careerLevel = CareerModel.GetLevelItem(pInfo.detail.careerId, pInfo.detail.level);
                pInfo.detail.currentHp = careerLevel.maxHp;
                pInfo.detail.currentMp = careerLevel.maxMp;

                roleBase.AddComponent<AIMoveController>();

                playerInfo.detail.DeductMp(skillInfo.mp);
            }
            else
            {
                Debug.Log("无法释放技能！");
            }
        }
    }
}
