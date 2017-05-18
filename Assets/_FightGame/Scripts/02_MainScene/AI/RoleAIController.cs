using UnityEngine;
using System.Collections;

public class RoleAIController : MonoBehaviour
{
    public enum AIType
    {
        STAY = 1,
        PATROL_FIELD,
        PATROL_CIRCLE
    }

    public enum State
    {
        IDLE,
        PATROL,
        CHASE
    }

    protected NavMeshAgent agent;
    protected FootmanStateMachine character;
    protected Camera aiCamera;
    protected PlayerInfo playerInfo;
    protected int aiTypeId;
    protected AITypeItem aiTypeInfo;
    protected SphereCollider aiCollider;//本AI的警戒范围

    protected State state;
    protected bool alive;

    protected int waypointIndex = 0;
    protected float patrolSpeed = 0.5f;
    protected float chaseSpeed = 1f;
    protected float waypointDistance;
    protected float warnRadius;
    protected float patrolGapTime;
    protected float cameraFar;

    protected Collider targetCollider;
    protected GameObject target;

    protected Plane[] planes;


    void Start()
    {
        init();
    }

    virtual protected void init()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponentInChildren<FootmanStateMachine>();
        aiCamera = GetComponentInChildren<Camera>();
        playerInfo = GetComponent<PlayerInfo>();
        aiCollider = GetComponent<SphereCollider>();

        agent.updatePosition = true;
        agent.updateRotation = true;

        //state = RoleAIController.State.PATROL;
        alive = true;

        RandomWaypointIndex();

        
    }

    protected IEnumerator FSM()
    {
        while (alive)
        {
            switch (state)
            {
                case State.IDLE:
                    Idle();
                    break;
                case State.PATROL:
                    Patrol();
                    break;
                case State.CHASE:
                    Chase();
                    break;
            }
            yield return null;
        }
    }

    virtual protected void Idle()
    {

    }

    virtual protected void Patrol()
    {
        agent.speed = patrolSpeed;
        if (Vector3.Distance(this.transform.position, AIModel.wayPoints[waypointIndex].transform.position) > 2)
        {
            agent.SetDestination(AIModel.wayPoints[waypointIndex].transform.position);
            character.Move(agent.desiredVelocity, agent.speed);
            //Debug.Log(playerInfo.playerName+"去目的地");
        }
        else if (Vector3.Distance(this.transform.position, AIModel.wayPoints[waypointIndex].transform.position) <= 2)
        {
            RandomWaypointIndex();
            //Debug.Log(playerInfo.playerName + "决定哪个目的地");
        }
        else
        {
            character.Move(Vector3.zero, 0);
        }
    }

    virtual protected void Chase()
    {
        //Debug.Log(playerInfo.playerName + "追击！");
        agent.speed = chaseSpeed;
        agent.SetDestination(target.transform.position);
        character.Move(agent.desiredVelocity, agent.speed);
    }

    virtual protected void MakeTargetPlayer()
    {
        if (targetCollider != null)
        {
            planes = GeometryUtility.CalculateFrustumPlanes(aiCamera);
            if (GeometryUtility.TestPlanesAABB(planes, targetCollider.bounds))
            {
                //Debug.Log(playerInfo.playerName + "看到这个人->" +targetCollider.name);
                //CheckForPlayer();
                state = State.CHASE;
                target = targetCollider.gameObject;
            }
        }
    }

    virtual protected void CheckPlayer(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer(SkillRef.PlayersLayer))
        {
            //Debug.Log(playerInfo.playerName + "  " + collider.name);
            targetCollider = collider;
        }
    }


    private void RandomWaypointIndex()
    {
        waypointIndex = Random.Range(0, AIModel.wayPoints.Length);
    }


}
