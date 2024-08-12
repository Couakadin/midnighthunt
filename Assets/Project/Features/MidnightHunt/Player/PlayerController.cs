using Data.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Midnighthunt.Runtime
{
    public class PlayerController : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake()
        {
            if (_playerBlackboard.ContainsKey("PlayerSpeed"))
                _playerSpeed = _playerBlackboard.GetValue<float>("PlayerSpeed");
        }

        private void FixedUpdate()
        {
            Vector3 velocity = _moveInput * _playerSpeed;
            _playerRigidbody.velocity = velocity;
        }

        #endregion

        #region Methods

        public void OnMoveInput(InputAction.CallbackContext context) => _moveInput = context.ReadValue<Vector3>();

        #endregion

        #region Utils

        #endregion

        #region Privates

        [Title("Components")]
        [SerializeField]
        private Blackboard _playerBlackboard;
        [SerializeField]
        private Rigidbody _playerRigidbody;
        
        private float _playerSpeed;

        private Vector3 _moveInput;

        #endregion
    }
}
