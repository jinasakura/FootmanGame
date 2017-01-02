using UnityEngine;
using System.Collections;

namespace FightDemo.ThirdPerson
{
    [RequireComponent(typeof(FootmanCharacter))]
    public class FootmanUserController : MonoBehaviour
    {
        //枚举被绑定到一种职业上了
        public enum OnceActionType { Jump=0, Attack01, Attack02, DoubleAttack, JumpAttack, CastSpell, TakeDamage};
        [SerializeField]
        private OnceActionType onceActionType = OnceActionType.Jump;

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
            _character.Move(h,v,(int)onceActionType);
            _character.Rotate(_moveRotation);
            _character.RotateCamera(_cameraRotationX);
        }

    }
}

