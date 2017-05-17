using UnityEngine;
using System.Collections;

public class RoleAIController : MonoBehaviour
{
    public enum State
    {
        IDLE,
        PATROL,
        CHASE
    }

    protected NavMeshAgent agent;
    protected FootmanStateMachine character;
    protected Camera aiCam;
    protected PlayerInfo playerInfo;
    protected int aiTypeId;
    protected AIType aiTypeInfo;

    protected State state;
    protected bool alive;

    protected int waypointIndex = 0;
    protected float patrolSpeed = 0.5f;
    protected float chaseSpeed = 1f;

    protected Collider targetCollider;
    protected GameObject target;

    protected Plane[] planes;


    void Start()
    {
        init();
    }

    protected void init()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponentInChildren<FootmanStateMachine>();
        aiCam = GetComponentInChildren<Camera>();
        playerInfo = GetComponent<PlayerInfo>();

        agent.updatePosition = true;
        agent.updateRotation = true;

        state = RoleAIController.State.PATROL;
        alive = true;

        RandomWaypointIndex();

        StartCoroutine("FSM");
    }

    IEnumerator FSM()
    {
        while (alive)
        {
            switch (state)
            {
                case State.IDLE:
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

    private void Patrol()
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

    private void Chase()
    {
        //Debug.Log(playerInfo.playerName + "追击！");
        agent.speed = chaseSpeed;
        agent.SetDestination(target.transform.position);
        character.Move(agent.desiredVelocity, agent.speed);
    }

    void Update()
    {
        if (targetCollider != null)
        {
            planes = GeometryUtility.CalculateFrustumPlanes(aiCam);
            if (GeometryUtility.TestPlanesAABB(planes, targetCollider.bounds))
            {
                //Debug.Log(playerInfo.playerName + "看到这个人->" +targetCollider.name);
                //CheckForPlayer();
                state = State.CHASE;
                target = targetCollider.gameObject;
            }
        }
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer(SkillRef.PlayersLayer))
        {
            Debug.Log(playerInfo.playerName + "  " + collider.name);
            targetCollider = collider;
        }
    }

    private void RandomWaypointIndex()
    {
        waypointIndex = Random.Range(0, AIModel.wayPoints.Length);
    }


}
