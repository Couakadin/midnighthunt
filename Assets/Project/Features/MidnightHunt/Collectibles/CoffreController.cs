using Data.Runtime;
using UnityEngine;

namespace Midnighthunt.Runtime
{
    public class CoffreController : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (_playerBlackboard.GetValue<int>("PlayerCollectible") == 4)
                {
                    _playerArm.SetActive(true);
                    _gameBlackboard.SetValue<bool>("GameOver", true);
                }
            }
        }

        #endregion

        #region Methods

        #endregion

        #region Utils

        #endregion

        #region Privates

        [SerializeField]
        private Blackboard _gameBlackboard;
        [SerializeField]
        private Blackboard _playerBlackboard;
        [SerializeField]
        private GameObject _playerArm;

        #endregion
    }
}
