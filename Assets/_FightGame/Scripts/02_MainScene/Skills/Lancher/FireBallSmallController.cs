using UnityEngine;
using System.Collections;

public class FireBallSmallController : FireBallController
{

    void FixedUpdate()
    {
        Vector3 endPoint = gameObject.transform.position;
        float distance = (endPoint - startPoint).magnitude;
        //Debug.Log(distance + "      " + skillInfo.releaseDistance);
        if (distance > skillInfo.releaseDistance)
        {
            if (gameObject != null) { Destroy(gameObject); }
        }
    }

    void OnTriggerStay(Collider enemyCollider)
    {
        //Debug.Log("lalalala->"+enemyCollider.gameObject.name);
        if (enemyCollider.gameObject.layer == LayerMask.NameToLayer(SkillRef.PlayersLayer))
        {
            GameObject enemy = enemyCollider.gameObject;
            ViableEntityInfo enemyInfo = enemy.GetComponentInParent<ViableEntityInfo>();
            if (enemyInfo != null)
            {
                enemyInfo.DeductHp(skillInfo.damageHp);
                if (gameObject != null) { Destroy(gameObject); }
            }
        }
    }

}
