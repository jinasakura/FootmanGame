using System;
using UnityEngine;
using System.Collections;

namespace FightDemo.ThirdPerson
{
    [RequireComponent(typeof(Rigidbody))]
    public class FootmanCharacter : MonoBehaviour
    {
        //[SerializeField]
        //private Transform groundObject;
        //[SerializeField]
        //private float heightOffsetOnGround;
        //private bool isGrounded;
        //为什么这些数值要放在Character里？
        //因为这是每个角色自身带的属性，每个角色都不一样，所以千万不能放到controller里
        [SerializeField]
        private float moveWalkSpeedMultiplier = 1f;
        [SerializeField]
        private float moveRunSpeedMultiplier = 1.5f;
        
        
        [SerializeField]
        private float stillOffset = 0.001f;//区分站立和走的偏移值
        [SerializeField]
        private float groundCheckDistance = 0.1f;//人物是否离地的偏移值
        //[SerializeField]
        //private float jumpPower = 5.0f;//人物跳起来的力
        [SerializeField]
        private bool _isLive = true;
        public bool isLive
        {
            get { return _isLive; }
            set { _isLive = value; }
        }
        private int _stayState = 0;

        private int _onceActionType = 1;
        public int onceActionType
        {
            get { return _onceActionType; }
            set { _onceActionType = value; }
        }

        private float speed = 0.0f;
        private bool _isTrigger = false;
        public bool isTrigger
        {
            get { return _isTrigger; }
            set { _isTrigger = value; }
        }
        //private bool isJump = false;
        //private bool inState = false;//是不是在动作进行中

        private Vector3 moveVelocity = Vector3.zero;

        private bool _isGrounded;//人物是否在地面上

        private Rigidbody rb;
        private Animator animator;

        private static int IDLE = 0;

        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody>();

            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.TriggerSkill);

        }

        public void Move(float h, float v)
        {
            if (!isLive)
            {
                Debug.Log("人物死亡！");
                return;
            }
            //CheckGroundStatus();

            float tmpH = Mathf.Abs(h);
            float tmpV = Mathf.Abs(v);
            //区分静止还是移动
            if (tmpH <= stillOffset && tmpV <= stillOffset)
            {
                _stayState = IDLE;
                speed = 0;
            }
            else
            {
                moveVelocity = transform.right * h + transform.forward * v;
                speed = Mathf.Max(tmpH, tmpV);
            }

            UpdateAnimation();
        }

        //public void HandleJumpMovement()
        //{
        //    Debug.Log("isJump->" + _isJump + "  isGrounded->" + _isGrounded + " inState->" + _inState);
        //    //隐患1：
        //    //直接对物体在某个轴上施加一个绝对大小的力，可能会被今后施加的其他力所影响
        //    //从而导致不能保证达到目前跳跃效果
        //    //问题2：没有判断是否处于完跳跃状态
        //    if (_isJump && _isGrounded && _inState)
        //    {
        //        Debug.Log("jump");
        //        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, jumpPower, _rigidbody.velocity.z);
        //        _isGrounded = false;
        //        _animation.applyRootMotion = false;
        //        groundCheckDistance = 0.1f;
        //    }
        //    UpdateAnimation();
        //}

        public void Die()
        {
            isLive = false;
            isTrigger = true;
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            animator.SetBool("isLive", _isLive);
            animator.SetInteger("stayState", _stayState);
            animator.SetFloat("speed", speed);
            animator.SetInteger("onceActionType", _onceActionType);
            if (isTrigger)
            {
                animator.SetTrigger("triggerOnceAction");
                isTrigger = false;
            }
        }


        private void MakeLive()
        {
            if (!isLive)
            {
                isLive = true;
                animator.SetBool("isLive", isLive);
            }
        }

        void FixedUpdate()
        {
            PerformMovement();
        }

        private void PerformMovement()
        {
            if (moveVelocity != Vector3.zero)
            {
                rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
            }
        }



        void CheckGroundStatus()
        {
            RaycastHit hitInfo;
#if UNITY_EDITOR
            // helper to visualise the ground check ray in the scene view
            Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundCheckDistance));
#endif
            // 0.1f is a small offset to start the ray from inside the character
            // it is also good to note that the transform position in the sample assets is at the base of the character
            if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
            {
                _isGrounded = true;
                //Debug.Log("在地面上");
                animator.applyRootMotion = true;
            }
            else
            {
                _isGrounded = false;
                //Debug.Log("不在地面上");
                animator.applyRootMotion = false;
            }
        }

        void TriggerSkill(NotificationCenter.Notification skillInfo)
        {
            SkillItem skill = (SkillItem)skillInfo.data;
            onceActionType = skill.skillId;
            isTrigger = true;
        }

        //void OnCollisionStay(Collision collisionInfo)
        //{
        //    bool isChild = collisionInfo.transform.IsChildOf(this.groundObject);
        //    if (isChild)
        //    {
        //        float otherObjectHeight = collisionInfo.transform.position.y;
        //        float thisObjectHeight = this.transform.position.y;
        //        if (thisObjectHeight - heightOffsetOnGround >= otherObjectHeight)
        //        {
        //            this.isGrounded = true;
        //            return;
        //        }
        //    }
        //    this.isGrounded = false;
        //}

    }
}

