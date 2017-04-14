using UnityEngine;
using System.Collections;

public class FirePointController : MonoBehaviour {


	void Start () {
        string oldName = gameObject.name;
        int playerId = GetComponentInParent<PlayerInfo>().playerId;

        string newName = oldName.Replace(SkillRef.FirePoint, SkillRef.FirePoint + playerId.ToString());
        gameObject.name = newName;

	}
	
}
