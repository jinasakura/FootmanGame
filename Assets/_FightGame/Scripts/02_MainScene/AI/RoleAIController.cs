using UnityEngine;
using System;
using System.Collections;

public class RoleAIController : MonoBehaviour
{

    public enum State
    {
        NONE,
        IDLE,
        PATROL,
        ATTACK
    }

    protected NavMeshAgent agent;
    protected PlayerStateMachine character;
    protected Camera aiCamera;
    protected PlayerInfo playerInfo;
    private RoleSkillController skillController;
    //protected AITypeItem aiTypeInfo;
    //protected SphereCollider aiCollider;//本AI的警戒范围

    private State _currentState = State.NONE;
    public State currentState
    {
        set
        {
            if (value != currentState)
            {
                _currentState = value;
                ChangeState();
            }
        }
        get { return _currentState; }
    }
    protected bool alive;

    public int[] waypoints;
    public GameObject[] targets;
    private int index = 0;
    public float patrolSpeed = 0.5f;
    public float chaseSpeed = 1f;
    //protected float waypointDistance;
    //protected float warnRadius;
    //protected float patrolGapTime;
    //protected float cameraFar;

    //protected Collider targetCollider;
    protected GameObject target;
    private bool gotoDes = false;//是不是到达目的地了

    protected Plane[] planes;


    void Start()
    {
        init();
    }

    virtual protected void init()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponentInChildren<PlayerStateMachine>();
        aiCamera = GetComponentInChildren<Camera>();
        playerInfo = GetComponent<PlayerInfo>();
        skillController = GetComponent<RoleSkillController>();
        //aiCollider = GetComponent<SphereCollider>();

        agent.updatePosition = true;
        agent.updateRotation = true;

        alive = true;
        if (AIModel.GetWaypoint(waypoints[index]) != null) { currentState = State.PATROL; }
        else
        {
            currentState = State.IDLE;
            Invoke("StartPatrol", 3f);
        }

    }

    private void StartPatrol()
    {
        currentState = State.PATROL;
    }

    void FixedUpdate()
    {
        ChangeState();
    }

    private void ChangeState()
    {
        if (alive)
        {
            switch (currentState)
            {
                case State.IDLE:
                    Idle();
                    break;
                case State.PATROL:
                    Patrol();
                    break;
                case State.ATTACK:
                    Attack();
                    break;
            }
        }
    }

    private void Idle()
    {
        character.Move(Vector3.zero, 0);
    }

    private GameObject des;
    protected void Patrol()
    {
        agent.speed = patrolSpeed;
        des = AIModel.GetWaypoint(waypoints[index]);
        target = targets[index];
        if (Vector3.Distance(transform.position, des.transform.position) >= 2)
        {
            agent.SetDestination(des.transform.position);
            character.Move(agent.desiredVelocity, agent.speed);
            gotoDes = false;
            //Debug.Log(playerInfo.playerName + "去目的地"+ waypoints[index]);
        }
        else
        {
            character.Move(Vector3.zero, 0);
            currentState = State.ATTACK;
            gotoDes = true;
            if (index == waypoints.Length - 1)
            {
                currentState = State.IDLE;
            }
            else
            {
                index++;
            }

        }
    }

    void Update()
    {
        if (currentState == State.ATTACK)
        {
            Vector3 fwd = des.transform.forward;
            fwd.y = 0;
            transform.rotation = Quaternion.LookRotation(fwd);
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (!gotoDes) return;
    //    if (other.gameObject.layer == LayerMask.NameToLayer(SkillRef.PlayersLayer))
    //    {
    //        planes = GeometryUtility.CalculateFrustumPlanes(aiCamera);
    //        if (GeometryUtility.TestPlanesAABB(planes, other.bounds))
    //        {
    //            Debug.Log(other.name + "进入视线");
    //            PlayerInfo otherInfo = other.gameObject.GetComponent<PlayerInfo>();
    //            if (otherInfo != null && otherInfo.camp != playerInfo.camp)
    //            {
    //                Debug.Log(other.name + "看着敌人");
    //                target = other.gameObject;
    //                transform.LookAt(other.gameObject.transform);
    //                //currentState = State.ATTACK;
    //            }

    //        }
    //    }
    //}


    private float lastTime;
    private void Attack()
    {
        //Debug.Log("shi fang ji neng");
        if (Time.fixedTime - lastTime > 3)
        {
            PlayerInfo info = target.GetComponent<PlayerInfo>();
            if (info.currentHp > 0)
            {
                //Debug.Log("shi fang ji neng");
                SkillLevelItem skill = SkillModel.FindFirstSkill(playerInfo.careerId);
                skillController.HandleSkill(skill.id);
                lastTime = Time.time;
            }
            else
            {
                currentState = State.PATROL;
            }
        }
    }



}
