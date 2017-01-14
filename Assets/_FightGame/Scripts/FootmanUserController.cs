using UnityEngine;
using System.Collections;

namespace FightDemo.ThirdPerson
{
    [RequireComponent(typeof(FootmanCharacter))]
    public class FootmanUserController : MonoBehaviour
    {
        
        //[SerializeField]
        //private OnceActionType onceActionType = OnceActionType.Attack01;

        private FootmanCharacter _character; // A reference to the ThirdPersonCharacter on the object
        private Transform _cam;                  // A reference to the main camera in the scenes transform
        private Vector3 _camForward;             // The current forward direction of the camera
        private Vector3 _moveVelocity = Vector3.zero;
        private Vector3 _moveRotation = Vector3.zero;

        void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                _cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            _character = GetComponent<FootmanCharacter>();
        }

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs - Modified by Kaz Crowe
            //float h = CrossPlatformInputManager.GetAxis( "Horizontal" );
            float h = UltimateJoystick.GetHorizontalAxis("Move");
            //float v = CrossPlatformInputManager.GetAxis( "Vertical" );
            float v = UltimateJoystick.GetVerticalAxis("Move");


            float _yRot = UltimateJoystick.GetHorizontalAxis("Look");
            _moveRotation = new Vector3(0f, _yRot, 0f);

            float _xRot = UltimateJoystick.GetVerticalAxis("Look");
            float _cameraRotationX = _xRot;

            // pass all parameters to the character control script
            _character.Move(h, v);
            _character.Rotate(_moveRotation);
            _character.RotateCamera(_cameraRotationX);
        }

        void Update()
        {
            //放到github上后出现了，点按钮没反应，改了按钮的名字解决了
            //按钮回头要改成根据职业变化，自动获取技能数据产生对应技能按钮
            //还要将代码和按钮关联起来
            //if (UltimateButton.GetButtonDown("Attack01Button")/* || Input.GetButtonDown("Jump")*/)
            //{
            //    _character.onceActionType = (int)OnceActionType.Attack01;
            //    _character.isTrigger = true;
            //    Debug.Log("Attack01Button");
            //}
            //if (UltimateButton.GetButtonDown("Attack02Button"))
            //{
            //    _character.onceActionType = (int)OnceActionType.Attack02;
            //    _character.isTrigger = true;
            //}
            //if (UltimateButton.GetButtonDown("DoubleAttackButton"))
            //{
            //    _character.onceActionType = (int)OnceActionType.DoubleAttack;
            //    _character.isTrigger = true;
            //}
            //if (UltimateButton.GetButtonDown("CastSpellButton"))
            //{
            //    _character.onceActionType = (int)OnceActionType.CastSpell;
            //    _character.isTrigger = true;
            //}
        }

    }
}

