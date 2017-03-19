using UnityEngine;

public class MoveState : RoleState
{

    //private Animator animator;
    //private StateMachineParams stateParams;

    [SerializeField]
    private float speedMultiplier = 2f;

    private Rigidbody rb;
    
    private bool canMove = false;


    protected override void init()
    {
        base.init();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

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
        rb.MovePosition(rb.position + stateParams.moveVelocity * speedMultiplier * Time.fixedDeltaTime);
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("移动");
        canMove = true;
    }

    public override void Exit()
    {
        base.Exit();
        //这里需要清一下数据，不然实际状态机的状态还是在move，而状态机代码实际已经是静止了
        //目前不知道为什么会残留有数据
        animator.SetFloat("speed", stateParams.speed);
        canMove = false;
    }

    public override void HandleParamers(object info)
    {
        base.HandleParamers(info);
        if (!stateParams.isLive)
            animator.SetBool("isLive", stateParams.isLive);
        animator.SetFloat("speed", stateParams.speed);
    }


}
