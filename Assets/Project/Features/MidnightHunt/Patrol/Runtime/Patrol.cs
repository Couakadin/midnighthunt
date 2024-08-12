using UnityEngine;

namespace Patrol.Runtime
{
    public class Patrol : MonoBehaviour
    {
        #region Publics



        #endregion


        #region Unity API



        #endregion


        #region Main Methods

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j;

                j = GetNextIndexWaypoint(i);

                Gizmos.color = Color.green;
                Gizmos.DrawSphere(transform.GetChild(i).position, 0.15f);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public int GetNextIndexWaypoint(int i)
        {
            if (i + 1 < transform.childCount)
            {
                return i + 1;
            }
            else
            {
                return 0;
            }
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }

        #endregion


        #region Utils

        #endregion


        #region Private & Protected


        #endregion
    }
}
