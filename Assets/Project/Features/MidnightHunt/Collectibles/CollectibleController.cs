using Data.Runtime;
using TMPro;
using UnityEngine;

namespace Midnighthunt.Runtime
{
    public class CollectibleController : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake() 
        { 
            _inputController = new InputController();

            if (_playerBlackboard.ContainsKey("PlayerCollectible"))
                _playerBlackboard.SetValue<int>("PlayerCollectible", 0);
        }

        private void OnEnable() => _inputController.Enable();

        private void OnDisable() => _inputController.Disable();

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") && IsCollected())
            {
                if (_playerBlackboard.GetValue<int>("PlayerCollectible") < 4)
                    _playerBlackboard.SetValue<int>("PlayerCollectible", _playerBlackboard.GetValue<int>("PlayerCollectible") + 1);

                gameObject.SetActive(false);
            }
        }

        #endregion

        #region Methods

        public bool IsCollected() => _inputController.Player.Attack.IsPressed();

        #endregion

        #region Utils

        #endregion

        #region Privates

        [SerializeField]
        private Blackboard _playerBlackboard;

        private InputController _inputController;

        #endregion
    }
}
