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
            _player = GameObject.FindWithTag("Player");
            //_fighter = gameObject.GetComponent<Fighter>();
        }
        private void Start()
        {
            _originPosition = transform.position;
        }

        private void Update()
        {
            //if (GetComponent<Health>().IsDead()) { return; }
            //if (IsInAttackRange() && _fighter.CanAttack(_player))
            //{
            //    AttackBehavior();
            //    _timeSinceLastSawPlayer = 0;
            //}
            //else 
            if (_timeSinceLastSawPlayer <= _suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void SuspicionBehavior()
        {
            //GetComponent<ActionScheduler>().CancelCurrentAction();
            Debug.Log("SuspicionBehavior");
        }

        public bool IsInAttackRange()
        {
            return Vector3.Distance(transform.position, _player.transform.position) < _chaseDistance;
        }
        
        private void AttackBehavior()
        {
            Debug.Log("AttackBehavior");
            //fighter.Attack(player);
        }

        #endregion


        #region Main Methods

        private void UpdateTimers()
        {
            _timeSinceLastSawPlayer += Time.deltaTime;
            _timeSinceWaypoint += Time.deltaTime;
        }

        #endregion


        #region Utils

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _chaseDistance);
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
                this.gameObject.GetComponent<CreepMoveBehaviour>().StartMoveAction(nextPosition, _patrolSpeedFraction);
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
            //Debug.Log($"GetWaypoint: {_patrolWaypointIndex} : {_patrolPath.GetWaypoint(_patrolWaypointIndex)} ");
            return _patrolPath.GetWaypoint(_patrolWaypointIndex);
        }

        private void CycleWaypoint()
        {
            _patrolWaypointIndex = _patrolPath.GetNextIndexWaypoint(_patrolWaypointIndex);
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
        int _patrolWaypointIndex = 0;
        float _timeSinceLastSawPlayer = Mathf.Infinity;
        float _timeSinceWaypoint = Mathf.Infinity;
        Vector3 _originPosition;

        #endregion

    }
}
