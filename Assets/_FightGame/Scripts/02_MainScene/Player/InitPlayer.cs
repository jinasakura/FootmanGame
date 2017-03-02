using UnityEngine;
using System.Collections.Generic;


public class InitPlayer : MonoBehaviour {

    //private GameObject userPlayer;

    [SerializeField]
    private GameObject playerCamera;

    [SerializeField]
    private int userControlNo = 0;//记录用户控制哪个角色

    public Transform[] respawns;

    [HideInInspector]
    public GameObject[] players;

    void Awake () {
        GameObject model = AllInfoModel.modelDict[LoginUserInfo.userCareer.modelName];

        if (Camera.main)
        {
            Camera mainCam = Camera.main;
            mainCam.gameObject.SetActive(false);
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
            character.playerInfo.currentHp = LoginUserInfo.userCareer.maxHealth;
            character.playerInfo.careerId = LoginUserInfo.userCareer.careerId;
            if (LoginUserInfo.playerInfo.playerId == character.playerInfo.playerId)
            {
                FootmanUserController userController = players[i].AddComponent<FootmanUserController>();
                Instantiate(playerCamera, playerCamera.transform.position, playerCamera.transform.rotation, players[i].transform);
                //character.orderInLayer = respawns.Length;
                //character.sortingLayerName = "UI-Self";
            }
            else
            {
                FootmanAIController aiController = players[i].AddComponent<FootmanAIController>();
            }
            i++;
        }


    }
	
	
}
