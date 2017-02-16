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

        this.name = "MoveState";
        //Debug.Log(this.name);
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            PerformMovement();
        }

    }

    private void PerformMovement()
    {
        if (stateParams.speed > PlayerDetail.StayOffset)
        {
            rb.MovePosition(rb.position + stateParams.moveVelocity * speedMultiplier * Time.fixedDeltaTime);
            //Debug.Log("确实移动 speed->" + stateParams.speed);
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

    void HandleParamers(NotificationCenter.Notification info)
    {
        stateParams = (StateMachineParams)info.data;
        animator.SetBool("isLive", stateParams.isLive);
        animator.SetFloat("speed", stateParams.speed);

        if (stateParams.playerId == 0)
            Debug.Log("===speed->" + stateParams.speed);//"---MoveState---playerId->" + stateParams.playerId + 
    }

}
