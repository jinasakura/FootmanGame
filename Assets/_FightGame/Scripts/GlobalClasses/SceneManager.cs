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

    [SerializeField]
    private GameObject playerCamera;

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
	
    public FootmanCharacter GetCharacterById(int playerId)
    {
        GameObject player = playersDict[playerId];
        return player.GetComponent<FootmanCharacter>();
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
        GameObject player;
        PlayerInfo playerInfo;
        foreach (Transform item in respawns)
        {
            player = Instantiate(model, item.transform.position, item.transform.rotation) as GameObject;
            playerInfo = player.GetComponent<PlayerInfo>();
            playerInfo.playerId = i;
            playerInfo.playerName = "Player " + i;
            playerInfo.careerId = LoginUserInfo.playerInfo.careerId;
            playerInfo.detail = new PlayerDetailInfo();
            playerInfo.detail.level = 1;
            player.AddComponent<FootmanCharacter>();
            if (LoginUserInfo.playerInfo.playerId == playerInfo.playerId)
            {
                FootmanUserController userController = player.AddComponent<FootmanUserController>();
                Instantiate(playerCamera, playerCamera.transform.position, playerCamera.transform.rotation, player.transform);
            }
            else
            {
                FootmanAIController aiController = player.AddComponent<FootmanAIController>();
            }
            playersDict[i] = player;

            i++;
            
        }
    }
	
}
