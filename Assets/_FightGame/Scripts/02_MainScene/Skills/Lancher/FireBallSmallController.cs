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
        Debug.Log("lalalala->"+enemyCollider.gameObject.name);
        if (enemyCollider.gameObject.layer == LayerMask.NameToLayer(SkillRef.PlayersLayer))
        {
            GameObject enemy = enemyCollider.gameObject;
            PlayerInfo enemyInfo = enemy.GetComponentInParent<PlayerInfo>();
            if (enemyInfo != null)
            {
                FootmanStateMachine role = enemy.GetComponent<FootmanStateMachine>();
                if (role != null)
                {
                    //Debug.Log("Attack   " + isAttack);
                    //playerInfo.detail.DeductMp(skillInfo.mp);
                    enemyInfo.DeductHp(skillInfo.damageHp);
                    //role.TakeDamageAction();
                    if (gameObject != null) { Destroy(gameObject); }
                }
            }
        }
    }

}
