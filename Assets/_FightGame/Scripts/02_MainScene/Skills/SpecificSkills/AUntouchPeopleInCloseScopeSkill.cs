using UnityEngine;
using System.Collections;

public class AUntouchPeopleInCloseScopeSkill : FarCombatSkill
{

    protected override void OnSkillFire(int skillId,bool fire)
    {
        base.OnSkillFire(skillId,fire);
        if (skillFireStart)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * skillInfo.damageRadius, Color.cyan, 10);

            Collider[] colliders = Physics.OverlapSphere(transform.position, skillInfo.damageRadius,LayerMask.NameToLayer(SkillRef.EnvironmentLayer));

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
                    if (enemyInfo != null && enemyInfo.id != LoginUserInfo.playerId)
                    {
                        PlayerStateMachine role = enemy.GetComponent<PlayerStateMachine>();
                        if (role != null)
                        {
                            //float damage = CalculateDamage(role.transform.position);
                            enemyInfo.DeductHp(skillInfo.damageHp);
                            //role.TakeDamageAction();
                        }
                    }
                }
                
            }
        }
    }


}
