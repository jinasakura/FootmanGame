using UnityEngine;
using System;
using System.Collections.Generic;


/// <summary>
/// 这有个大冲突：如果通过Start初始化后，通过instance的public方法访问player数据会被清空
/// 所以这里的职责只是MainScene初始化，数据存储和访问放到别的地方
/// </summary>
public class InitMainScene : MonoBehaviour {

    private static string RespawnTag = "Respawn";
    private static string WayPoints = "AIWaypoint";

    //摄像机这个prefab改了三遍了
    //为什么放到这里：因为放到controller里我没法（不知道）对prefab在运行时赋值
    [SerializeField]
    private GameObject cameraPrefab;

    [SerializeField]
    private GameObject RoleBasePrefab;
    [SerializeField]
    private GameObject AIRoleBasePrefab;

    [SerializeField]
    private GameObject[] models;

    private GameObject[] respawns;//测试使用，出生点位置

    [SerializeField]
    private GameObject[] lanchers;


    void Start ()
    {
        HandleModelInfo();
        initAIInfo();
        //随机N个地点(随后加上)
        initAllPlayers();
        
        initAllLanchers();
    }

    private void HandleModelInfo()
    {
        //初始化模型数据
        foreach (GameObject item in models)
        {
            PlayerModel.SetModelReference(item.name, item);
        }
    }

    private void initAllPlayers()
    {
        GameObject model = PlayerModel.GetModelByName(LoginUserInfo.modelName);
        PlayerModel.roleBasePrefab = RoleBasePrefab;
        respawns = GameObject.FindGameObjectsWithTag(RespawnTag);
        //Array.Sort(respawns);

        GameObject playerModel;
        PlayerInfo playerInfo;
        GameObject roleBase;
        int i = 0;
        foreach (GameObject item in respawns)
        {
            if (i == 0)
            {
                roleBase = Instantiate(RoleBasePrefab, respawns[i].transform.position, respawns[i].transform.rotation) as GameObject;
                playerInfo = roleBase.GetComponent<PlayerInfo>();
                playerInfo.playerName = LoginUserInfo.playerName;
                //Instantiate(cameraPrefab, cameraPrefab.transform.position, cameraPrefab.transform.rotation, roleBase.transform);
                //roleBase.AddComponent<UserMoveController>();
                playerModel = Instantiate(model, respawns[i].transform.position, respawns[i].transform.rotation, roleBase.transform) as GameObject;
                ChangeLayer.SetLayerRecursively(playerModel, LayerMask.NameToLayer(SkillRef.PlayersLayer));
            }
            else
            {
                roleBase = Instantiate(AIRoleBasePrefab, respawns[i].transform.position, respawns[i].transform.rotation) as GameObject;
                playerInfo = roleBase.GetComponent<PlayerInfo>();
                playerInfo.playerName = "Player AI "+i;
                playerModel = Instantiate(model, respawns[i].transform.position, respawns[i].transform.rotation, roleBase.transform) as GameObject;
                ChangeLayer.SetLayerRecursively(playerModel, LayerMask.NameToLayer(SkillRef.AILayer));
            }
            
            
           
            playerInfo.playerId = i;
            playerInfo.modelName = LoginUserInfo.modelName;
            playerInfo.detail = new PlayerDetailInfo();
            playerInfo.detail.careerId = LoginUserInfo.careerId;
            playerInfo.detail.level = LoginUserInfo.level;
            CareerItem careerLevel = CareerModel.GetLevelItem(playerInfo.detail.careerId, playerInfo.detail.level);
            playerInfo.detail.currentHp = careerLevel.maxHp;
            playerInfo.detail.currentMp = careerLevel.maxMp;

            PlayerModel.SetPlayerInfo(playerInfo.playerId, playerInfo);
            i++;
        }
        NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.MainSceneIsReady);
    }

    private void initAIInfo()
    {
        AIModel.wayPoints = GameObject.FindGameObjectsWithTag(WayPoints);
        AIModel.AIRoleBasePrefab = AIRoleBasePrefab;

    }

    private void initAllLanchers()
    {
        foreach (GameObject item in lanchers)
        {
            if (item != null)
            {
                LanchersController.SetLancher(item.name, item);
            }
        }
    }


}
