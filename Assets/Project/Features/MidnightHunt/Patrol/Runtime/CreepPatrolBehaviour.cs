using UnityEngine;

namespace Patrol.Runtime
{
    public class CreepPatrolBehaviour : MonoBehaviour
    {
        #region Publics

        #endregion


        #region Unity API

        private void Awake()
        {
            _fighterState = GetComponent<CreepAttackBehaviour>();
        }
        private void Start()
        {
            _playerLayerMask = LayerMask.GetMask("Player");
            _originPosition = transform.position;
        }

        private void Update()
        {
            if (IsInChaseRange())
            {
                AttackBehavior();
                _timeSinceLastSawPlayer = 0;
            }
            else if (_timeSinceLastSawPlayer <= _suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                PatrolBehaviour();
            }
            _lightBlackboard.SetValue<bool>("IsInDangerZone", IsInChaseRange());
            UpdateTimers();
        }

        private void SuspicionBehavior()
        {
            GetComponent<CreepState>().CancelCurrentAction();
            Debug.Log("SuspicionBehavior");
        }

        public bool IsInChaseRange()
        {
            _objectInDetectionRange = Physics.OverlapSphere(transform.position, _chaseDistance, _playerLayerMask);
            _player = null;
            foreach (var obj in _objectInDetectionRange)
            {
                _player = obj.gameObject;
            }
            if(_player != null) return Vector3.Distance(transform.position, _player.transform.position) < _chaseDistance;
            return false;
        }
        
        private void AttackBehavior()
        {
            Debug.Log("AttackBehavior");
            _fighterState.Attack(_player);

        }

        #endregion


        #region Main Methods

        private void UpdateTimers()
        {
            _timeSinceLastSawPlayer += Time.deltaTime;
            _timeSinceWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = _originPosition;
            if (_patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();

            }
            if (_timeSinceWaypoint > _pauseBetweenWaypoint)
            {
                GetComponent<CreepMoveBehaviour>().StartMoveAction(nextPosition, _patrolSpeedFraction);
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(GetCurrentWaypoint(), transform.position);
            if (distanceToWaypoint < _waypointDistanceTolerance) { _timeSinceWaypoint = 0; return true; }
            return false;
        }

        private Vector3 GetCurrentWaypoint()
        {
            return _patrolPath.GetWaypoint(_patrolWaypointIndex);
        }

        private void CycleWaypoint()
        {
            _patrolWaypointIndex = _patrolPath.GetNextIndexWaypoint(_patrolWaypointIndex);
        }

        #endregion


        #region Utils

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
        }

        #endregion


        #region Private & Protected

        [SerializeField] float _chaseDistance = 5f;
        [SerializeField] float _suspicionTime = 3f;
        [SerializeField] float _pauseBetweenWaypoint = 1.5f;
        [SerializeField] float _waypointDistanceTolerance = 1.5f;
        [SerializeField] float _patrolSpeedFraction = 0.4f;
        [SerializeField] GameObject _player;
        [SerializeField] Patrol _patrolPath;
        [SerializeField] Data.Runtime.Blackboard _lightBlackboard;
        Collider[] _objectInDetectionRange;
        int _patrolWaypointIndex = 0;
        int _playerLayerMask;
        float _timeSinceLastSawPlayer = Mathf.Infinity;
        float _timeSinceWaypoint = Mathf.Infinity;
        Vector3 _originPosition;
        CreepAttackBehaviour _fighterState;

        #endregion

    }
}
