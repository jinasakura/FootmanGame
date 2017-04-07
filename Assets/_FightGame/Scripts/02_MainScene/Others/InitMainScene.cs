using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// 这有个大冲突：如果通过Start初始化后，通过instance的public方法访问player数据会被清空
/// 所以这里的职责只是MainScene初始化，数据存储和访问放到别的地方
/// </summary>
public class InitMainScene : MonoBehaviour {

    //摄像机这个prefab改了三遍了
    //为什么放到这里：因为放到controller里我没法（不知道）对prefab在运行时赋值
    [SerializeField]
    private GameObject cameraPrefab;

    [SerializeField]
    private GameObject roleBasePrefab;

    [SerializeField]
    private GameObject[] models;
    [SerializeField]
    private Transform[] respawns;//测试使用，出生点位置

    public GameObject fireBall;


    void Start ()
    {
        HandleModelInfo();
        //随机N个地点(随后加上)
        initAllPlayers();
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

        int i = 0;
        GameObject playerModel;
        PlayerInfo playerInfo;
        GameObject roleBase;
        foreach (Transform item in respawns)
        {
            roleBase = Instantiate(roleBasePrefab, item.transform.position, item.transform.rotation) as GameObject;
            playerModel = Instantiate(model, item.transform.position, item.transform.rotation, roleBase.transform) as GameObject;
            playerInfo = roleBase.GetComponent<PlayerInfo>();
            playerInfo.playerId = i;
            //playerInfo.playerName = "Player " + i;
            playerInfo.detail = new PlayerDetailInfo();
            playerInfo.detail.careerId = LoginUserInfo.careerId;
            playerInfo.detail.level = LoginUserInfo.level;
            CareerItem careerLevel = CareerModel.GetLevelItem(playerInfo.detail.careerId, playerInfo.detail.level);
            playerInfo.detail.currentHp = careerLevel.maxHp;
            playerInfo.detail.currentMp = careerLevel.maxMp;
            if (LoginUserInfo.playerId == playerInfo.playerId)
            {
                playerInfo.playerName = LoginUserInfo.playerName;
                Instantiate(cameraPrefab, cameraPrefab.transform.position, cameraPrefab.transform.rotation, roleBase.transform);
                roleBase.AddComponent<UserMoveController>();
            }
            else
            {
                playerInfo.playerName = "Player " + i;
                roleBase.AddComponent<AIMoveController>();
            }

            //playersDict[i] = roleBase;
            //playerInfoDict[playerInfo.playerId] = playerInfo;
            PlayerModel.SetPlayerInfo(playerInfo.playerId, playerInfo);

            i++;
        }
        NotificationCenter.DefaultCenter.PostNotification(this, MainSceneEvent.MainSceneIsReady);
    }



}
