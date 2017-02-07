using UnityEngine;

public class MoveState : State {

    private Animator animator;

    [SerializeField]
    private float speedMultiplier = 2f;

    private Rigidbody rb;
    private StateMachineParams stateParams;


    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        this.name = "MoveState";
    }

    void FixedUpdate()
    {
        PerformMovement();
    }

    private void PerformMovement()
    {
        if (stateParams.speed > CharacterInfo.stayOffset)
        {
            rb.MovePosition(rb.position + stateParams.moveVelocity * speedMultiplier * Time.fixedDeltaTime);
        }
    }

    protected override void AddListeners()
    {
        NotificationCenter.DefaultCenter.AddObserver(this, StateMachineEvent.HandleParamers);
    }

    protected override void RemoveListeners()
    {
        NotificationCenter.DefaultCenter.RemoveObserver(this, StateMachineEvent.HandleParamers);
    }

    void HandleParamers(NotificationCenter.Notification info)
    {
        stateParams = (StateMachineParams)info.data;
        animator.SetFloat("speed", stateParams.speed);
        
        //Debug.Log("moveSpeed"+stateParams.speed);
    }

}
