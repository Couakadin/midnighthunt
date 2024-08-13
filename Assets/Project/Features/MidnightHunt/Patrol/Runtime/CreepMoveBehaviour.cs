using UnityEngine;
using UnityEngine.AI;

namespace Patrol.Runtime
{
    public class CreepMoveBehaviour : MonoBehaviour, IAction
    {
        #region Publics

        #endregion


        #region Unity API

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _lodGroup = GetComponent<LODGroup>();
            _animators = new Animator[_lodGroup.lodCount];
            for (int i = 0; i < _lodGroup.lodCount; i++)
            {
                _animators[i] = _lodGroup.GetLODs()[i].renderers[0].GetComponentInParent<Animator>();
            }
        }

        private void Update()
        {
            int lodIndex = GetCurrentLODIndex();
            if (lodIndex != _currentLODIndex) _currentLODIndex = lodIndex;
            UpdateAnimation();
        }

        #endregion


        #region Main Methods

        private int GetCurrentLODIndex()
        {
            LOD[] lods = _lodGroup.GetLODs();
            for (int i = 0; i < lods.Length; i++)
            {
                if (lods[i].renderers[0].isVisible) return i;
            }
            return 0;
        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<CreepState>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            _navMeshAgent.destination = destination;
            //_navMeshAgent.speed = _maxSpeed * Mathf.Clamp01(speedFraction);
            _navMeshAgent.isStopped = false;
        }

        private void UpdateAnimation()
        {
            Vector3 veloce = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(veloce);
            float speed = localVelocity.z;
            _currentLodAnimator = _animators[_currentLODIndex];
            _currentLodAnimator.SetFloat("ForwardSpeed", speed);
        }

        #endregion


        #region Utils

        #endregion


        #region Private & Protected

        [SerializeField] float _maxSpeed = 6f;
        NavMeshAgent _navMeshAgent;
        LODGroup _lodGroup;
        Animator[] _animators;
        Animator _currentLodAnimator;
        int _currentLODIndex = 0;

        #endregion
    }
}
