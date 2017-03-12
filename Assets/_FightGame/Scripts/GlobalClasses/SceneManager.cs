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
        FootmanCharacter character;
        GameObject player;
        foreach (Transform item in respawns)
        {
            player = Instantiate(model, item.transform.position, item.transform.rotation) as GameObject;
            character = player.GetComponent<FootmanCharacter>();
            character.playerInfo = new PlayerInfo();//回头改成从一个本地xml中读取玩家信息
            character.playerInfo.playerId = i;
            character.playerInfo.playerName = "Player " + i;
            character.playerInfo.careerId = LoginUserInfo.playerInfo.careerId;
            character.playerInfo.level = 1;
            if (LoginUserInfo.playerInfo.playerId == character.playerInfo.playerId)
            {
                FootmanUserController userController = players[i].AddComponent<FootmanUserController>();
                Instantiate(playerCamera, playerCamera.transform.position, playerCamera.transform.rotation, players[i].transform);
            }
            else
            {
                FootmanAIController aiController = players[i].AddComponent<FootmanAIController>();
            }
            playersDict[i] = player;

            i++;
            
        }
    }
	
}
