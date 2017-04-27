using UnityEngine;
using System.Collections;

public class AUntouchPeopleInCloseScopeSkill : RoleSkill
{

    protected override void OnSkillFire(bool fire)
    {
        base.OnSkillFire(fire);
        if (skillFireStart)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * skillInfo.damageRadius, Color.cyan, 10);

            Collider[] colliders = Physics.OverlapSphere(transform.position, skillInfo.damageRadius);

            //int num = Mathf.Max(colliders.Length, skillInfo.damagePeople);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders.Length == skillInfo.damagePeople) break;

                if (colliders[i].gameObject.layer == LayerMask.NameToLayer(SkillRef.PlayersLayer) && colliders[i].gameObject.tag != SkillRef.WeaponTag)
                {
                    Rigidbody targetRb = colliders[i].gameObject.GetComponentInParent<Rigidbody>();
                    if (!targetRb) continue;
                    //targetRb.AddExplosionForce(explosionForce, transform.position, skillInfo.damageRadius);

                    GameObject enemy = colliders[i].gameObject;
                    PlayerInfo enemyInfo = enemy.GetComponentInParent<PlayerInfo>();
                    if (enemyInfo != null && enemyInfo.playerId != LoginUserInfo.playerId)
                    {
                        FootmanStateMachine role = enemy.GetComponent<FootmanStateMachine>();
                        if (role != null)
                        {
                            //float damage = CalculateDamage(role.transform.position);
                            enemyInfo.detail.DeductHp(skillInfo.damageHp);
                            role.TakeDamageAction();
                        }
                    }
                }
                
            }
        }
    }


}
