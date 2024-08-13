using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

namespace Midnighthunt.Runtime
{
    public class ProtoBeast : MonoBehaviour
    {
        #region Publics


        #endregion


        #region unity API

        private void Awake()
        {
            _agentCreep = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _agentCreep.destination = _moveCreepToPosition.position;
        }    

        #endregion


        #region main methods



        #endregion


        #region private

        [SerializeField] Transform _moveCreepToPosition;
        NavMeshAgent _agentCreep;

        #endregion
    }
}
