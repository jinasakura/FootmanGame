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

    private GameObject target;

    private GameObject player;
    private Collider playerCollider;
    private Camera aiCam;
    private Plane[] planes;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponentInChildren<FootmanStateMachine>();
        aiCam = GetComponentInChildren<Camera>();
        playerCollider = GetComponent<SphereCollider>();

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
        if (Vector3.Distance(this.transform.position, AIModel.wayPoints[wayPointIndex].transform.position) >= 2)
        {
            agent.SetDestination(AIModel.wayPoints[wayPointIndex].transform.position);
            character.Move(agent.desiredVelocity, agent.speed);
        }
        else if(Vector3.Distance(this.transform.position, AIModel.wayPoints[wayPointIndex].transform.position) <= 2){
            RandomWaypointIndex();
        }
        else
        {
            character.Move(Vector3.zero, 0);
        }
    }

    private void Chase()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(target.transform.position);
        character.Move(agent.desiredVelocity, agent.speed);
    }

    void Update()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(aiCam);
        if (GeometryUtility.TestPlanesAABB(planes, playerCollider.bounds))
        {
            Debug.Log(playerCollider.name);
            //CheckForPlayer();
        }
        //else
        //{

        //}
    }

    void CheckForPlayer()
    {
        RaycastHit hit;
        Debug.DrawRay(aiCam.transform.position, transform.forward * 10, Color.green);
        if(Physics.Raycast(aiCam.transform.position,transform.forward,out hit, 10))
        {
            state = State.CHASE;
            target = hit.collider.gameObject;
        }
    }

    //void OnTriggerEnter(Collider collider)
    //{
    //    if (collider.gameObject.layer == LayerMask.NameToLayer(SkillRef.PlayersLayer))
    //    {
    //        state = PlayerAIController.State.INVESTIGATE;
    //        target = collider.gameObject;
    //    }
    //}

    private void RandomWaypointIndex()
    {
        wayPointIndex = Random.Range(0, AIModel.wayPoints.Length);
    }


}
