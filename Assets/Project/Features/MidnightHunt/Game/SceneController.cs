using Data.Runtime;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Midnighthunt.Runtime
{
    public class SceneController : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake()
        {
            Cursor.visible = true;
        }

        private void Update()
        {
            if (_gameBlackboard.GetValue<bool>("GameOver"))
                FadeOut();
        }

        #endregion

        #region Methods

        public void StartGame() => SceneManager.LoadScene("MainScene");

        public void ExitGame() 
        {
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        }

        public void FadeOut() => _animator?.SetBool("FadeOut", true);

        public void OnFadeComplete() => SceneManager.LoadScene("StartScene");

        #endregion

        #region Utils

        #endregion

        #region Privates

        [SerializeField]
        [CanBeNull]
        private Animator? _animator;
        [SerializeField]
        private Blackboard _gameBlackboard;

        #endregion
    }
}
