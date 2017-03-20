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

    public Transform[] respawns;

    [HideInInspector]
    public GameObject[] players;

    private Dictionary<int, GameObject> playersDict;
    //据说要在统一一个地方去刷新所有动作？

    void Awake ()
    {
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
        GameObject model = AllInfoModel.modelDict[LoginUserInfo.playerInfo.modelName];
        playersDict = new Dictionary<int, GameObject>();
        //if (Camera.main)
        //{
        //    Camera mainCam = Camera.main;
        //    mainCam.gameObject.SetActive(false);
        //}

        int i = 0;
        GameObject playerGo;
        PlayerInfo playerInfo;
        
        foreach (Transform item in respawns)
        {
            playerGo = Instantiate(model, item.transform.position, item.transform.rotation) as GameObject;
            playerInfo = playerGo.GetComponent<PlayerInfo>();
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
                Instantiate(cameraPrefab, cameraPrefab.transform.position, cameraPrefab.transform.rotation, playerGo.transform);
                FootmanRoleController input = playerGo.AddComponent<FootmanRoleController>();
            }
            else
            {
                FootmanAIController aiInput = playerGo.AddComponent<FootmanAIController>();
            }
            
            playersDict[i] = playerGo;

            i++;
            
        }
    }
	
}
