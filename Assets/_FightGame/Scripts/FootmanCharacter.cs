using System;
using UnityEngine;
using System.Collections;

namespace FightDemo.ThirdPerson
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class FootmanCharacter : MonoBehaviour
    {
        [SerializeField]
        private Transform groundObject;
        [SerializeField]
        private float heightOffsetOnGround;
        private bool isGrounded;
        //为什么这些数值要放在Character里？
        //因为这是每个角色自身带的属性，每个角色都不一样，所以千万不能放到controller里
        [SerializeField]
        private float _moveWalkSpeedMultiplier = 1f;
        [SerializeField]
        private float _moveRunSpeedMultiplier = 1.5f;
        [SerializeField]
        private Camera _cam;
        [SerializeField]
        private float _cameraRotationLimit = 90f;//最终的数值由外面决定
        [SerializeField]
        private float _lookSensitivity = 3f;//镜头上下旋转的系数
        [SerializeField]
        private float _stillOffset = 0.001f;//区分站立和走的偏移值
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

        private float _speed = 0.0f;
        private bool _isTrigger = false;
        public bool isTrigger
        {
            get { return _isTrigger; }
            set { _isTrigger = value; }
        }
        private bool _isJump = false;
        private bool _inState = false;//是不是在动作进行中

        private Vector3 _moveVelocity = Vector3.zero;
        private Vector3 _rotation = Vector3.zero;
        private float _cameraRotationX = 0f;
        private float _currentCameraRotationX = 0f;

        private bool _isGrounded;//人物是否在地面上

        private Rigidbody _rigidbody;
        private Animator _animation;

        private static int IDLE = 0;

        void Start()
        {
            _animation = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();

            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            _currentCameraRotationX = _cam.transform.localEulerAngles.x;

            //NotificationCenter.DefaultCenter.AddObserver(this, "OnOnceActionStateBegin");
            //NotificationCenter.DefaultCenter.AddObserver(this, "OnOnceActionStateEnd");

            //initSkillButtons();
        }

        public void Rotate(Vector3 rotation)
        {
            _rotation = rotation;
        }

        public void RotateCamera(float cameraRotationX)
        {
            _cameraRotationX = cameraRotationX;
        }

        public void Move(float h, float v)
        {
            if (!isLive)
            {
                Debug.Log("人物死亡！");
                return;
            }
            CheckGroundStatus();

            float tmpH = Mathf.Abs(h);
            float tmpV = Mathf.Abs(v);
            //区分静止还是移动
            if (tmpH <= _stillOffset && tmpV <= _stillOffset)
            {
                _stayState = IDLE;
                _speed = 0;
            }
            else
            {
                _moveVelocity = transform.right * h + transform.forward * v;
                _speed = Mathf.Max(tmpH, tmpV);
            }

            //_onceActionType = onceActionType;

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
            _isLive = false;
            _isTrigger = true;
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            _animation.SetBool("isLive", _isLive);
            _animation.SetInteger("stayState", _stayState);
            _animation.SetFloat("speed", _speed);
            _animation.SetInteger("onceActionType", _onceActionType);
            if (_isTrigger)
            {
                _animation.SetTrigger("triggerOnceAction");
                _isTrigger = false;
            }
        }


        private void MakeLive()
        {
            if (!isLive)
            {
                isLive = true;
                _animation.SetBool("isLive", isLive);
            }
        }

        void FixedUpdate()
        {
            PerformMovement();
            PerformRotation();
        }

        private void PerformMovement()
        {
            if (_moveVelocity != Vector3.zero)
            {
                _rigidbody.MovePosition(_rigidbody.position + _moveVelocity * Time.fixedDeltaTime);
            }
        }

        private void PerformRotation()
        {
            Quaternion q = _rigidbody.rotation * Quaternion.Euler(_rotation);
            _rigidbody.MoveRotation(q);
            if (_cam != null)
            {
                _currentCameraRotationX -= _cameraRotationX;
                _currentCameraRotationX = Mathf.Clamp(_currentCameraRotationX, -_cameraRotationLimit, _cameraRotationLimit);

                _cam.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0f, 0f);
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no Player camera found.");
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
                _animation.applyRootMotion = true;
            }
            else
            {
                _isGrounded = false;
                //Debug.Log("不在地面上");
                _animation.applyRootMotion = false;
            }
        }

        void OnOnceActionStateBegin(NotificationCenter.Notification notification)
        {
            int stateType = Convert.ToInt32(notification.data);
            //Debug.Log("OnOnceActionStateBegin: stateType->" + stateType);
            //if (stateType == 0)
            //{
            //    _isJump = true;
            //}
            //else
            //{
            //    _isJump = false;
            //}
            _inState = true;
            //HandleJumpMovement();
        }

        void OnOnceActionStateEnd(NotificationCenter.Notification notification)
        {
            //Debug.Log("OnOnceActionStateEnd");
            //_isJump = false;
            _inState = false;
        }

        void OnCollisionStay(Collision collisionInfo)
        {
            bool isChild = collisionInfo.transform.IsChildOf(this.groundObject);
            if (isChild)
            {
                float otherObjectHeight = collisionInfo.transform.position.y;
                float thisObjectHeight = this.transform.position.y;
                if (thisObjectHeight - heightOffsetOnGround >= otherObjectHeight)
                {
                    this.isGrounded = true;
                    return;
                }
            }
            this.isGrounded = false;
        }

        void initSkillButtons()
        {
            GameObject[] btns = GameObject.FindGameObjectsWithTag("SkillButton");
            int i = 0;
            foreach (GameObject btn in btns)
            {
                SkillTextButton textBtn = btn.GetComponent<SkillTextButton>();
                textBtn.btnName = Career.kightSkills[i].skillName;
                textBtn.onceActionType = Career.kightSkills[i].skillType;
                textBtn.character = this;
            }
        }
    }
}

