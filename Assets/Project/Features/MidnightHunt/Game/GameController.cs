using Data.Runtime;
using UnityEngine;

namespace Midnighthunt.Runtime
{
    public class GameController : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake() => _gameBlackboard.SetValue<bool>("GameOver", false);

        private void Update()
        {
            if (_gameBlackboard.GetValue<bool>("GameOver"))
            {
                Vector3 targetAngles = _playerController.transform.eulerAngles + 180f * Vector3.up;

                _playerController.GetInputController().Disable();
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
        private PlayerController _playerController;

        #endregion
    }
}
