using UnityEngine;
using System;
using System.Collections.Generic;


/// <summary>
/// 这有个大冲突：如果通过Start初始化后，通过instance的public方法访问player数据会被清空
/// 所以这里的职责只是MainScene初始化，数据存储和访问放到别的地方
/// </summary>
public class InitMainScene : MonoBehaviour
{

    private static string RespawnTag = "HeroRespawn";
    private static string SoliderRespawnTag = "SoliderRespawn";
    private static string WayPoints = "AIWaypoints";

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
    [SerializeField]
    private GameObject respawn;

    [SerializeField]
    private GameObject[] lanchers;


    void Start()
    {
        HandleModelInfo();
        initUser();
        //initAIPlayers();
        initAllLanchers();

        NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.MainSceneIsReady);
    }

    private void HandleModelInfo()
    {
        //初始化模型数据
        foreach (GameObject item in models)
        {
            PlayerModel.SetModelReference(item.name, item);
        }
    }

    //这里只初始化用户
    private void initUser()
    {
        GameObject model = PlayerModel.GetModelByName(LoginUserInfo.modelName);
        PlayerModel.roleBasePrefab = RoleBasePrefab;
        //Array.Sort(respawns);

        GameObject playerModel;
        PlayerInfo playerInfo;
        GameObject roleBase = Instantiate(RoleBasePrefab, respawn.transform.position, respawn.transform.rotation) as GameObject;
        playerInfo = roleBase.GetComponent<PlayerInfo>();
        playerInfo.eName = LoginUserInfo.playerName;
        //Instantiate(cameraPrefab, cameraPrefab.transform.position, cameraPrefab.transform.rotation, roleBase.transform);
        //roleBase.AddComponent<UserMoveController>();
        playerModel = Instantiate(model, respawn.transform.position, respawn.transform.rotation, roleBase.transform) as GameObject;
        ChangeLayer.SetLayerRecursively(playerModel, LayerMask.NameToLayer(SkillRef.PlayersLayer));

        playerInfo.id = 1000001;
        playerInfo.modelName = LoginUserInfo.modelName;
        playerInfo.careerId = LoginUserInfo.careerId;
        playerInfo.level = LoginUserInfo.level;
        CareerItem careerLevel = CareerModel.GetLevelItem(playerInfo.careerId, playerInfo.level);
        playerInfo.currentHp = careerLevel.maxHp;
        playerInfo.currentMp = careerLevel.maxMp;

        PlayerModel.SetPlayerInfo(playerInfo.id, playerInfo);
    }

    private void initAIPlayers()
    {
        //AIModel.wayPoints = GameObject.FindGameObjectsWithTag(WayPoints);
        //AIModel.AIRoleBasePrefab = AIRoleBasePrefab;
        GameObject[] pointArray = GameObject.FindGameObjectsWithTag(WayPoints);
        foreach (GameObject item in pointArray)
        {
            AIModel.SetWaypoints(Int32.Parse(item.name), item);
        }

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
