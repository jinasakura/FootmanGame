using UnityEngine;
using System.Collections;

public class RoleSkillController : MonoBehaviour {

	void Start () {
        NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.ReleaseSkill);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
