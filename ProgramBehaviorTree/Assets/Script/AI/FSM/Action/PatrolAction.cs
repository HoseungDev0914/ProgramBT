using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions
{

    public class PatrolAction : ActionTask
    {

        public BBParameter<NavMeshAgent> navAgent;
        public BBParameter<int> patrolIndex;

        private readonly Vector3[] patrolPoints = new Vector3[]
        {
        new Vector3(-50, 0, 40),
        new Vector3(-50, 0, -40),
        new Vector3(-15, 0, -40),
        new Vector3(-15, 0, 40),
        new Vector3(16, 0, 40),
        new Vector3(16, 0, -40),
        new Vector3(50, 0, -40),
        new Vector3(50, 0, 40)
        };

        protected override void OnExecute()
        {
            int index = patrolIndex.value % patrolPoints.Length;
            navAgent.value.SetDestination(patrolPoints[index]);
        }

        protected override void OnUpdate()
        {
            if (navAgent.value == null || !navAgent.value.isOnNavMesh) return;

            if (!navAgent.value.pathPending && navAgent.value.remainingDistance <= navAgent.value.stoppingDistance)
            {
                EndAction(true); // 도착했으므로 FSM에서 Scan 상태로 전이
            }
        }
    }
}