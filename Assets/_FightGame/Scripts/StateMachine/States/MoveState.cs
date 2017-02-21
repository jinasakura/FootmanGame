using UnityEngine;

public class MoveState : State
{

    private Animator animator;

    [SerializeField]
    private float speedMultiplier = 2f;

    private Rigidbody rb;
    private StateMachineParams stateParams;
    private bool canMove = false;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        stateParams = GetComponent<StateMachineParams>();
        this.name = /*stateParams.playerId+*/"MoveState";
        //Debug.Log(this.name);
    }

    void FixedUpdate()
    {
        //if (stateParams != null)
        //    Debug.Log("确实移动 speed->" + stateParams.speed);
        if (canMove)
        {
            PerformMovement();
        }

    }

    private void PerformMovement()
    {
        //Debug.Log("确实移动 speed->" + stateParams.speed);
        if (stateParams.speed > PlayerDetail.StayOffset)
        {
            rb.MovePosition(rb.position + stateParams.moveVelocity * speedMultiplier * Time.fixedDeltaTime);
        }
    }

    protected override void AddListeners()
    {
        NotificationCenter.DefaultCenter.AddObserver(this, StateMachineEvent.HandleParamers);
        canMove = true;
        //Debug.Log("可以移动");
    }

    protected override void RemoveListeners()
    {
        NotificationCenter.DefaultCenter.RemoveObserver(this, StateMachineEvent.HandleParamers);
        canMove = false;
        //Debug.Log("不能移动");
    }

    void HandleParamers()//NotificationCenter.Notification info
    {
        //stateParams = (StateMachineParams)info.data;
        //if (stateParams.playerId == 0)
        //{
        if (!stateParams.isLive)
            animator.SetBool("isLive", stateParams.isLive);
        animator.SetFloat("speed", stateParams.speed);
        //Debug.Log("speed->" + stateParams.speed);
        //}
        //if (stateParams.playerId == 0)
        //    Debug.Log("MoveState里------speed->" + stateParams.speed);//"---MoveState---playerId->" + stateParams.playerId + 
    }

}
