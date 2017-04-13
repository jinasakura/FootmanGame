using UnityEngine;
using System.Collections;

public class FireBallLargeController : FireBallController {

    [SerializeField]
    private LayerMask damageMask;
    public float explosionForce;

    void OnTriggerEnter(Collider enemyCollider)
    {
        if (enemyCollider.gameObject.layer == LayerMask.NameToLayer(SkillRef.GroundLayer))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, skillInfo.damageRadius, damageMask);
            int num = Mathf.Min(colliders.Length, skillInfo.damagePeople);
            for (int i = 0; i < num; i++)
            {
                Rigidbody targetRb = colliders[i].gameObject.GetComponentInParent<Rigidbody>();
                if (!targetRb) continue;
                targetRb.AddExplosionForce(explosionForce, transform.position, skillInfo.damageRadius);

                GameObject enemy = colliders[i].gameObject;
                PlayerInfo enemyInfo = enemy.GetComponentInParent<PlayerInfo>();
                if (enemyInfo != null && enemyInfo.playerId != LoginUserInfo.playerId)
                {
                    FootmanStateMachine role = enemy.GetComponent<FootmanStateMachine>();
                    if (role != null)
                    {
                        float damage = CalculateDamage(role.transform.position);
                        enemyInfo.detail.DeductHp(damage);
                        role.TakeDamageAction();
                    }
                }
            }
            Destroy(gameObject);
        }
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        // Calculate the distance from the shell to the target.
        float explosionDistance = explosionToTarget.magnitude;

        // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
        float relativeDistance = (skillInfo.damageRadius - explosionDistance) / skillInfo.damageRadius;

        // Calculate damage as this proportion of the maximum possible damage.
        float damage = relativeDistance * skillInfo.damageHp;

        // Make sure that the minimum damage is always 0.
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}
