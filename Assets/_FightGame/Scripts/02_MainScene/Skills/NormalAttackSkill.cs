using UnityEngine;
using System;

public class NormalAttackSkill : RoleSkill
{


    //这个地方有个bug，如果没有这个变量，会出现第一次攻击，血量改变一次90；第二次攻击，血量改变两次80、70；
    //第三次攻击，血量改变三次60、50、40
    //初步怀疑是OnTriggerStay被多次调用，为什么这样暂时不知道
    private bool isAttack = false;

    //没事别乱用继承，这样会被调用两次
    void OnTriggerStay(Collider enemyCollider)
    {
        if (skillFire)
        {
            //Debug.Log("Attack");
            //从攻击者的角度来看
            if (enemyCollider.gameObject.layer == LayerMask.NameToLayer("Player")
                && enemyCollider.gameObject.tag == "Body")
            {
                GameObject enemy = enemyCollider.gameObject;
                PlayerInfo enemyInfo = enemy.GetComponentInParent<PlayerInfo>();
                if (enemyInfo != null)
                {
                    FootmanStateMachine role = enemy.GetComponent<FootmanStateMachine>();
                    if (role != null && !isAttack)
                    {
                        playerInfo.detail.DeductMp(skillLevel.mp);
                        enemyInfo.detail.DeductHp(skillLevel.damageHp);
                        role.TakeDamageAction();
                        CloseWeapon();
                        isAttack = true;
                    }
                }
            }
        }
    }
}
