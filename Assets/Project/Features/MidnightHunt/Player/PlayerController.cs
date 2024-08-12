using Data.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Midnighthunt.Runtime
{
    public class PlayerController : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake()
        {
            _inputController = new InputController();

            _camera = Camera.main;

            Cursor.visible = false;

            if (_playerBlackboard.ContainsKey("PlayerSpeed"))
                _playerSpeed = _playerBlackboard.GetValue<float>("PlayerSpeed");
            if (_playerBlackboard.ContainsKey("JumpHeight"))
                _jumpHeight = _playerBlackboard.GetValue<float>("JumpHeight");
            if (_playerBlackboard.ContainsKey("GravityValue"))
                _gravityValue = _playerBlackboard.GetValue<float>("GravityValue");
            if (_playerBlackboard.ContainsKey("GroundedPlayer"))
                _isGroundedPlayer = _playerBlackboard.GetValue<bool>("GroundedPlayer");
        }

        private void FixedUpdate()
        {
            Vector3 velocity = _camera.transform.forward * GetPlayerMovement().z + _camera.transform.right * GetPlayerMovement().x;
            _playerRigidbody.velocity = velocity * _playerSpeed;
            _playerRigidbody.velocity = new Vector3(_playerRigidbody.velocity.x, 0f, _playerRigidbody.velocity.z);

            Transform modelTransform = _model.transform;
            modelTransform.localRotation = modelTransform.parent.rotation;
        }

        private void OnEnable() => _inputController.Enable();

        private void OnDisable() => _inputController.Disable();

        #endregion

        #region Methods

        public Vector3 GetPlayerMovement() => _inputController.Player.Move.ReadValue<Vector3>();
        
        public Vector3 GetMouseDelta() => _inputController.Player.Look.ReadValue<Vector3>();

        public bool IsJumping() => _inputController.Player.Jump.triggered;

        #endregion

        #region Utils

        #endregion

        #region Privates

        [Title("Components")]
        [SerializeField]
        private Blackboard _playerBlackboard;
        [SerializeField]
        private Rigidbody _playerRigidbody;
        [SerializeField]
        private CharacterController _characterController;
        [SerializeField]
        private Light _lantern;
        [SerializeField]
        private GameObject _model;

        private InputController _inputController;

        private Camera _camera;

        private float _jumpHeight;
        private float _playerSpeed;
        private float _gravityValue;

        private bool _isGroundedPlayer;
 
        #endregion
    }
}
