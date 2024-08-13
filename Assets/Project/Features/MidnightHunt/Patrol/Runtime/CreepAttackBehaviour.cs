using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

namespace Patrol.Runtime
{
    public class CreepAttackBehaviour : MonoBehaviour, IAction
    {
        #region Publics

        #endregion


        #region Unity API

        private void Start()
        {
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

            _timeSinceLastAttack += Time.deltaTime;

            if (_target != null)
            {
                bool notInRange = Vector3.Distance(_target.transform.position, transform.position) > _weaponRange;
                if (notInRange)
                {
                    GetComponent<CreepMoveBehaviour>().MoveTo(_target.transform.position, 0.8f);
                }
                else
                {
                    GetComponent<CreepMoveBehaviour>().Cancel();
                    AttackBehaviour();
                }
            }
        }

        #endregion


        #region Main Methods

        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform);
            if (_timeSinceLastAttack <= _timeBetweenAttack) return;
            TriggerAttack();
            _timeSinceLastAttack = 0;
        }
        
        private void TriggerAttack()
        {
            _currentLodAnimator.ResetTrigger("CancelAttack");
            _currentLodAnimator.SetTrigger("Attack");
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<CreepState>().StartAction(this);
            _target = combatTarget;
        }

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
            GetComponent<CreepMoveBehaviour>().Cancel();
            StopAttack();
        }
        
        private void StopAttack()
        {
            _currentLodAnimator.SetTrigger("CancelAttack");
            _currentLodAnimator.ResetTrigger("Attack");
        }

        private void UpdateAnimation()
        {
            _currentLodAnimator = _animators[_currentLODIndex];
        }

        #endregion


        #region Utils

        #endregion


        #region Private & Protected

        [SerializeField] float _weaponRange = 0.3f;
        [SerializeField] float _timeBetweenAttack = 2f;
        GameObject _target;
        LODGroup _lodGroup;
        Animator[] _animators;
        Animator _currentLodAnimator;
        int _currentLODIndex = 0;
        float _timeSinceLastAttack;

        #endregion

    }
}
