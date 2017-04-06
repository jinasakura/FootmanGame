using UnityEngine;
using System.Collections;

public class AMeleeAttackSkill : RoleSkill {

    private bool onceAttack = false;
    private GameObject ball;

    void FixedUpdate()
    {
        if (skillActionStart)
        {
            if (ball == null)
            {
                ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                ball.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }

        }
    }
}
