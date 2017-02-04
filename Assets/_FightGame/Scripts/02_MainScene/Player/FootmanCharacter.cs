using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class FootmanCharacter : MonoBehaviour
{
    [SerializeField]
    private float speedMultiplier = 2f;
    [SerializeField]
    private bool _isLive = true;
    public bool isLive
    {
        get { return _isLive; }
        set { _isLive = value; }
    }

    private int stayState = 0;

    private int onceActionType = 1;

    private float speed = 0.0f;
    private bool isTrigger = false;

    public static float stayOffset = 0.001f;//区分站立和走的临界数
    private Vector3 moveVelocity = Vector3.zero;

    private Rigidbody rb;
    //private Animator animator;
    private CharacterStateMachine stateMachine;
    private StateMachineParams stateParams;


    void Start()
    {
        //animator = GetComponentInChildren<Animator>();
        stateMachine = new CharacterStateMachine(GetComponentInChildren<Animator>());
        stateParams = new StateMachineParams();

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.TriggerSkill);
        NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.CharacterLive);

    }

    public void Move(float h, float v)
    {
        float tmpH = Mathf.Abs(h);
        float tmpV = Mathf.Abs(v);
        //区分静止还是移动
        if (tmpH <= stayOffset && tmpV <= stayOffset)
        {
            speed = 0;
        }
        else
        {
            moveVelocity = transform.right * h + transform.forward * v;
            speed = Mathf.Max(tmpH, tmpV);
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        //animator.SetBool("isLive", _isLive);
        //animator.SetInteger("stayState", stayState);
        //animator.SetFloat("speed", speed);
        //animator.SetInteger("onceActionType", _onceActionType);
        //if (isTrigger)
        //{
        //    animator.SetTrigger("triggerOnceAction");
        //    isTrigger = false;
        //}
        stateParams.isLive = isLive;
        stateParams.stayState = stayState;
        stateParams.speed = speed;
        stateParams.onceActionType = onceActionType;
        stateParams.triggerOnceAction = isTrigger;

        stateMachine.stateParams = stateParams;

        if (isTrigger)
        {
            isTrigger = false;
            stateParams.triggerOnceAction = isTrigger;
            stateMachine.stateParams = stateParams;
        }

    }


    void FixedUpdate()
    {
        PerformMovement();
    }

    private void PerformMovement()
    {
        if (speed > stayOffset)
        {
            rb.MovePosition(rb.position + moveVelocity * speedMultiplier * Time.fixedDeltaTime);
        }
    }

    void TriggerSkill(NotificationCenter.Notification skillInfo)
    {
        SkillItem skill = (SkillItem)skillInfo.data;
        onceActionType = skill.skillId;
        isTrigger = true;
    }

    void CharacterLive(NotificationCenter.Notification liveInfo)
    {
        //人物生死通过抛事件获得
        //重置生死后
        UpdateAnimation();
    }


}

