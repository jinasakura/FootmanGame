using UnityEngine;
using System.Collections;

public class StayAIController : RoleAIController {

    private float lastTime = 0f;
    private Rigidbody rb;

    protected override void init()
    {
        base.init();

        rb = GetComponent<Rigidbody>();

        state = State.PATROL;

        aiTypeId = (int)RoleAIController.AIType.STAY;
        aiTypeInfo = AIModel.GetAiTypeInfo(aiTypeId);

        waypointIndex = aiTypeInfo.waypointIndex;
        warnRadius = aiTypeInfo.warnRadius;
        patrolGapTime = aiTypeInfo.patrolGapTime;
        cameraFar = aiTypeInfo.cameraFar;

        aiCamera.farClipPlane = cameraFar;
        aiCollider.radius = warnRadius;

        StartCoroutine("FSM");
    }


    protected override void Patrol()
    {
        if (Vector3.Distance(this.transform.position, AIModel.wayPoints[waypointIndex].transform.position) > aiTypeInfo.waypointDistance)
        {
            agent.SetDestination(AIModel.wayPoints[waypointIndex].transform.position);
            character.Move(agent.desiredVelocity, aiTypeInfo.patrolSpeed);
            Debug.Log(playerInfo.playerName + "去目的地");
        }
        else
        {
            state = State.IDLE;
        }
    }


    protected override void Idle()
    {
        character.Move(Vector3.zero, 0);
    }

    //void Update()
    //{
    //    if (Time.time - lastTime > patrolGapTime)//超过时间要转向
    //    {
    //        Vector3 offset = new Vector3(0f, 0.5f, 0f);
    //        //四元数相乘代表什么？
    //        Quaternion q = rb.rotation * Quaternion.Euler(offset);
    //        rb.MoveRotation(q);
    //    }
    //    else
    //    {
    //        lastTime = Time.time;
    //    }

    //    MakeTargetPlayer();
    //}

    void OnTriggerEnter(Collider collider)
    {
        CheckPlayer(collider);
    }

}
