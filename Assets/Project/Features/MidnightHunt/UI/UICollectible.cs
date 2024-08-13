using Data.Runtime;
using TMPro;
using UnityEngine;

namespace Midnighthunt.Runtime
{
    public class UICollectible : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Update() => _textCollectible.text = new string($"{_playerBlackboard.GetValue<int>("PlayerCollectible")}/4 items");

        #endregion

        #region Methods

        #endregion

        #region Utils

        #endregion

        #region Privates

        [SerializeField]
        private Blackboard _playerBlackboard;
        [SerializeField]
        private TextMeshProUGUI _textCollectible;

        #endregion
    }
}
