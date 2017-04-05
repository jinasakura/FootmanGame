using UnityEngine;
using System.Collections.Generic;

public class PlayerModel  {

    //将模型名和模型对应起来
    private static Dictionary<string, GameObject> modelDict = new Dictionary<string, GameObject>();
    //private static Dictionary<int, GameObject> playersDict = new Dictionary<int, GameObject>();
    private static Dictionary<int, PlayerInfo> playerInfoDict = new Dictionary<int, PlayerInfo>();

    //public static FootmanStateMachine GetCharacterById(int playerId)
    //{
    //    GameObject player = playersDict[playerId];
    //    return player.GetComponent<FootmanStateMachine>();
    //}

    public static void SetPlayerInfo(int playerId,PlayerInfo info)
    {
        if (!playerInfoDict.ContainsKey(playerId))
        {
            playerInfoDict[playerId] = info;
        }
        else { Debug.Log("已经有PlayerId->" + playerId + " 的Info了！"); }
    }

    public static PlayerInfo GetPlayerInfoInfoById(int playerId)
    {
        return playerInfoDict[playerId];
    }

    public static void SetModelReference(string modelName,GameObject model)
    {
        if (!modelDict.ContainsKey(modelName))
        {
            modelDict[modelName] = model;
        }
        else { Debug.Log(modelName + " 有重名的！"); }
    }

    public static GameObject GetModelByName(string modelName)
    {
        if (!modelDict.ContainsKey(modelName)) { Debug.Log("找不到這個模型" + modelName); }
        return modelDict[modelName];
    }

}
