using UnityEngine;
using System.Collections.Generic;


public class InitPlayer : MonoBehaviour {

    private GameObject userPlayer;

    [SerializeField]
    private GameObject respawn2;

    private GameObject testPlayer;

    void Start () {
        GameObject model = CareerInfoModel.modelDict[LoginUserInfo.userCareer.modelName];
        userPlayer = Instantiate(model, Vector3.zero, Quaternion.identity) as GameObject;

        //testPlayer=Instantiate(model, respawn2.transform.position, respawn2.transform.rotation) as GameObject;
    }
	
	
}
