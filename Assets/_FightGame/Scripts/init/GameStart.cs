using UnityEngine;
using System.Collections;
using FightDemo.ThirdPerson;

public class GameStart : MonoBehaviour {

    public GameObject thirdPersonController;
	// Use this for initialization
	void Start () {
        GameObject obj = Instantiate(thirdPersonController, Vector3.zero, Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
