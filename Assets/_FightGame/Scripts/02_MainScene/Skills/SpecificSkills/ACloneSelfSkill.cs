using UnityEngine;
using System.Collections;


/// <summary>
/// 在前面复制出另一个自己
/// </summary>
public class ACloneSelfSkill : RoleSkill
{

    private GameObject channelPlayer;

    protected override void OnSkillFire(int skillId,bool fire)
    {
        base.OnSkillFire(skillId,fire);

        if (skillFireStart)
        {
            Vector3 position = transform.position;
            position.y += 0.5f;
            Ray ray = new Ray(position, transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * skillInfo.releaseDistance, Color.cyan, 10);

            RaycastHit hitInfo = new RaycastHit();
            if (!Physics.Raycast(ray, out hitInfo, skillInfo.releaseDistance))
            {
                Vector3 newPlayerPosition = transform.forward * skillInfo.releaseDistance / 2 + transform.position;

                GameObject model = PlayerModel.GetModelByName(playerInfo.modelName);
                channelPlayer = Instantiate(PlayerModel.roleBasePrefab, newPlayerPosition, transform.rotation) as GameObject;
                channelPlayer.gameObject.name = "Player"+playerInfo.playerId.ToString()+ "Channeling";
                GameObject playerModel = Instantiate(model, newPlayerPosition, transform.rotation, channelPlayer.transform) as GameObject;
                PlayerInfo pInfo = channelPlayer.GetComponent<PlayerInfo>();
                pInfo.playerId = playerInfo.playerId;
                playerInfo.playerName = "Player channeling";
                pInfo.modelName = playerInfo.modelName;
                pInfo.detail = new PlayerDetailInfo();
                pInfo.detail.careerId = LoginUserInfo.careerId;
                pInfo.detail.level = LoginUserInfo.level;
                CareerItem careerLevel = CareerModel.GetLevelItem(pInfo.detail.careerId, pInfo.detail.level);
                pInfo.detail.currentHp = careerLevel.maxHp;
                pInfo.detail.currentMp = careerLevel.maxMp;

                //channelPlayer.AddComponent<AIMoveController>();
                Destroy(channelPlayer, 5);

                
            }
            else
            {
                Debug.Log("ACloneSelfSkill：无法释放技能——>Channeling");
            }
        }
    }
}
