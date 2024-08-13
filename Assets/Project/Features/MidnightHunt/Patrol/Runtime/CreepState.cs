using UnityEngine;

namespace Patrol.Runtime
{
    public class CreepState : MonoBehaviour
    {
        #region Publics

        #endregion


        #region Unity API

        #endregion


        #region Main Methods
        public void StartAction(IAction action)
        {
            if (_currentAction == action) return;
            if (_currentAction != null)
            {
                _currentAction.Cancel();
            }
            _currentAction = action;
        }
        public void CancelCurrentAction()
        {
            StartAction(null);
        }

        #endregion


        #region Utils

        #endregion

        
        #region Private & Protected

        [SerializeField] 
        IAction _currentAction;

        #endregion
    }
}
