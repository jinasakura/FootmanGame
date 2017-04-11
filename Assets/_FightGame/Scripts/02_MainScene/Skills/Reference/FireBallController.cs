using UnityEngine;
using System.Collections;

public class FireBallController : MonoBehaviour
{
    private float speed = 4;

    private Rigidbody rb;
    private int playerId;
    private float range;
    private Vector3 startPoint;
    private SkillLevelItem _skillInfo;
    public SkillLevelItem skillInfo { private get; set; }

    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    //Transform direction = GetComponentInParent<RoleSkillController>().gameObject.transform;
    //    //rb.velocity = direction.forward * speed;
    //    //startPoint = gameObject.transform.position;
    //}

    public void Fire(Transform start)
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = start.forward * speed;
        startPoint = start.position;
        //Debug.Log("velocity->" + rb.velocity + "   startPoint->" + startPoint);
    }

    public void SetOwnerId(int id)
    {
        playerId = id;
    }

    //public void SetRange(float r)
    //{
    //    range = r;
    //}

    void FixedUpdate()
    {
        Vector3 endPoint = gameObject.transform.position;
        float distance = (endPoint - startPoint).magnitude;
        //Debug.Log(distance+"      "+range);
        if (distance > skillInfo.distance)
        {
            if (gameObject != null) { Destroy(gameObject); }
        }
    }

    void OnTriggerStay(Collider enemyCollider)
    {
        if (enemyCollider.gameObject.layer == LayerMask.NameToLayer("Player")
                && enemyCollider.gameObject.tag == "Body")
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
                    enemyInfo.detail.DeductHp(skillInfo.damageHp);
                    role.TakeDamageAction();
                    if (gameObject != null) { Destroy(gameObject); }
                }
            }
        }
    }

}
