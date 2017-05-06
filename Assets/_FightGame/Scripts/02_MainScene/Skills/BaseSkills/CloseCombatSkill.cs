using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// 近战职业技能基类，需要有武器
/// </summary>
public class CloseCombatSkill : RoleSkill {

    protected List<Collider> weaponColliders;

    public override void init()
    {
        base.init();
        Collider[] colliders = GetComponentsInChildren<CapsuleCollider>();
        weaponColliders = new List<Collider>();
        foreach (Collider item in colliders)
        {
            //Debug.Log(item.name);
            if (item.gameObject.tag == SkillRef.WeaponTag)
            {
                //weaponCollider = item;
                weaponColliders.Add(item);
                item.enabled = false;//只能把武器的关了，body关了人就掉下去了
            }
        }
    }

    protected override void OnSkillFire(int skillId, bool fire)
    {
        base.OnSkillFire(skillId, fire);
        if (weaponColliders.Count != 0)
        {
            foreach (Collider item in weaponColliders)
            {
                item.enabled = fire;
            }
        }
    }

    protected virtual void CloseWeapon()
    {
        //if (weaponCollider != null) { weaponCollider.enabled = false; }
        foreach (Collider item in weaponColliders)
        {
            item.enabled = false;
        }
    }
}
