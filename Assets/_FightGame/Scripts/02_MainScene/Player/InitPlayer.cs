using UnityEngine;
using System.Collections.Generic;


public class InitPlayer : MonoBehaviour {

    private GameObject userPlayer;

    [SerializeField]
    private int userControlNo = 0;//记录用户控制哪个角色

    public Transform[] respawns;

    [HideInInspector]
    public GameObject[] players;

    void Awake () {
        GameObject model = CareerInfoModel.modelDict[LoginUserInfo.userCareer.modelName];
        //userPlayer = Instantiate(model, Vector3.zero, Quaternion.identity) as GameObject;
        //testPlayer=Instantiate(model, respawn2.transform.position, respawn2.transform.rotation) as GameObject;

        if (Camera.main)
        {
            Camera mainCam = Camera.main;
            mainCam.enabled = false;
        }

        int i = 0;
        FootmanCharacter character;
        players = new GameObject[respawns.Length];
        foreach (Transform item in respawns)
        {
            players[i] = Instantiate(model, item.transform.position, item.transform.rotation) as GameObject;
            character = players[i].GetComponent<FootmanCharacter>();
            character.playerInfo = new PlayerInfo();//回头改成从一个本地xml中读取玩家信息
            character.playerInfo.playerId = i;
            character.playerInfo.playerName = "Player " + i;
            //character.isLive = true;
            if (LoginUserInfo.playerInfo.playerId == character.playerInfo.playerId)
            {
                FootmanUserController userController = players[i].AddComponent<FootmanUserController>();
                userController.playerCamera = players[i].GetComponentInChildren<Camera>();
                //character.isUserControl = true;
            }
            else
            {
                FootmanAIController aiController = players[i].AddComponent<FootmanAIController>();
                players[i].GetComponentInChildren<Camera>().enabled = false;
                //character.isUserControl = false;

            }
            i++;
            //character.enabled = true;
        }


    }
	
	
}
