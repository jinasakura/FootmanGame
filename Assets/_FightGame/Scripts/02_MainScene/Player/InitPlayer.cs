using UnityEngine;
using System.Collections.Generic;


public class InitPlayer : MonoBehaviour {

    private GameObject player;
    void Start () {
        GameObject model = CareerInfoModel.modelDict[LoginUserInfo.userCareer.modelName];
        player = Instantiate(model, Vector3.zero, Quaternion.identity) as GameObject;
    }
	
	
}
