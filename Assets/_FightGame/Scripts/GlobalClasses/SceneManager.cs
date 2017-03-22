using UnityEngine;
using System.Collections.Generic;


public class SceneManager : MonoBehaviour {

    private static SceneManager _instance;
    public static SceneManager instance
    {
        get
        {
            if (!_instance)
            {
                GameObject scene = new GameObject("Default Scene Manager");
                _instance = scene.AddComponent<SceneManager>();
            }

            return _instance;
        }
    }

    //摄像机这个改了三遍了
    //为什么放到这里：因为放到controller里我没法（不知道）对prefab在运行时赋值
    [SerializeField]
    private GameObject cameraPrefab;

    [SerializeField]
    private GameObject roleBasePrefab;

    [SerializeField]
    private GameObject[] models;

    //将模型名和模型对应起来
    public Dictionary<string, GameObject> modelDict;

    public Transform[] respawns;

    [HideInInspector]
    public GameObject[] players;

    private Dictionary<int, GameObject> playersDict;
    //据说要在统一一个地方去刷新所有动作？

    void Awake ()
    {
        HandleModelInfo();
        //随机N个地点(随后加上)
        initAllPlayers();
    }
	
    public FootmanStateMachine GetCharacterById(int playerId)
    {
        GameObject player = playersDict[playerId];
        return player.GetComponent<FootmanStateMachine>();
    }

    private void initAllPlayers()
    {
        GameObject model = modelDict[LoginUserInfo.playerInfo.modelName];
        playersDict = new Dictionary<int, GameObject>();

        int i = 0;
        GameObject playerGo;
        PlayerInfo playerInfo;
        GameObject roleBase;
        foreach (Transform item in respawns)
        {
            roleBase = Instantiate(roleBasePrefab, item.transform.position, item.transform.rotation) as GameObject;
            playerGo = Instantiate(model, item.transform.position, item.transform.rotation, roleBase.transform) as GameObject;
            playerInfo = roleBase.GetComponent<PlayerInfo>();
            playerInfo.playerId = i;
            playerInfo.playerName = "Player " + i;
            playerInfo.careerId = LoginUserInfo.playerInfo.careerId;
            playerInfo.detail = new PlayerDetailInfo();
            playerInfo.detail.level = 1;
            CareerLevelItem careerLevel = CareerModel.GetLevelItem(playerInfo.careerId, playerInfo.detail.level);
            playerInfo.detail.currentHp = careerLevel.maxHp;
            playerInfo.detail.currentMp = careerLevel.maxMp;
            if (LoginUserInfo.playerInfo.playerId == playerInfo.playerId)
            {
                Instantiate(cameraPrefab, cameraPrefab.transform.position, cameraPrefab.transform.rotation, roleBase.transform);
                FootmanRoleController input = roleBase.AddComponent<FootmanRoleController>();
            }
            else
            {
                FootmanAIController aiInput = roleBase.AddComponent<FootmanAIController>();
            }
            
            playersDict[i] = roleBase;

            i++;
            
        }
    }

    private void HandleModelInfo()
    {
        //初始化模型数据
        modelDict = new Dictionary<string, GameObject>();
        foreach (GameObject item in models)
        {
            modelDict[item.name] = item;
        }
    }

}
