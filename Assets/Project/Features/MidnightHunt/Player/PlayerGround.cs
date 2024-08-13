using Codice.CM.Common;
using Data.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Midnighthunt.Runtime
{
    public class PlayerGround : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake()
        {
            _inputController = new InputController();

            _playerBody = _playerController.GetComponent<Rigidbody>();

            if (_playerBlackboard.ContainsKey("GroundedPlayer"))
                _playerBlackboard.SetValue<bool>("GroundedPlayer", true);
            if (_playerBlackboard.ContainsKey("JumpHeight"))
                _jumpHeight = _playerBlackboard.GetValue<float>("JumpHeight");
            if (_playerBlackboard.ContainsKey("GravityValue"))
                _gravityValue = _playerBlackboard.GetValue<float>("GravityValue");
        }

        private void OnEnable() => _inputController.Enable();

        private void OnDisable() => _inputController.Disable();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
                _playerBlackboard.SetValue<bool>("GroundedPlayer", true);
        }

        private void Update()
        {
            if (IsJumping() && _playerBlackboard.GetValue<bool>("GroundedPlayer"))
            {
                _playerBlackboard.SetValue<bool>("GroundedPlayer", false);
                _playerBody.AddForce(transform.up * _jumpHeight, ForceMode.Impulse);
            }
        }

        #endregion

        #region Methods

        public bool IsJumping() => _inputController.Player.Jump.IsPressed();

        #endregion

        #region Utils

        #endregion

        #region Privates

        [Title("Components")]
        [SerializeField]
        private Blackboard _playerBlackboard;
        [SerializeField]
        private PlayerController _playerController;

        private InputController _inputController;

        private Rigidbody _playerBody;

        private float _jumpHeight;
        private float _gravityValue;

        #endregion
    }
}
