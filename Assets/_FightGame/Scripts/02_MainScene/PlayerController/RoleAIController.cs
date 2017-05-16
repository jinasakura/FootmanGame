using UnityEngine;
using System.Collections;

public class RoleAIController : MonoBehaviour
{
    private NavMeshAgent agent;
    private FootmanStateMachine character;

    public enum State
    {
        PATROL,
        CHASE
    }

    private State state;
    private bool alive;

    private int wayPointIndex = 0;
    [SerializeField]
    private float patrolSpeed = 0.5f;
    [SerializeField]
    private float chaseSpeed = 1f;

    //private GameObject player;
    private Collider targetCollider;
    private GameObject target;

    private Camera aiCam;
    private Plane[] planes;
    private PlayerInfo playerInfo;

    void Start()
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
        if (Vector3.Distance(this.transform.position, AIModel.wayPoints[wayPointIndex].transform.position) > 2)
        {
            agent.SetDestination(AIModel.wayPoints[wayPointIndex].transform.position);
            character.Move(agent.desiredVelocity, agent.speed);
            //Debug.Log(playerInfo.playerName+"去目的地");
        }
        else if (Vector3.Distance(this.transform.position, AIModel.wayPoints[wayPointIndex].transform.position) <= 2)
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

    //void CheckForPlayer()
    //{
    //    RaycastHit hit;
    //    Debug.DrawRay(aiCam.transform.position, transform.forward * 10, Color.green);
    //    if(Physics.Raycast(aiCam.transform.position,transform.forward,out hit, 10))
    //    {
    //        state = State.CHASE;
    //        target = hit.collider.gameObject;
    //    }
    //}

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
        wayPointIndex = Random.Range(0, AIModel.wayPoints.Length);
    }


}
