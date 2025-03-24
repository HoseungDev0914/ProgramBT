using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;

namespace NodeCanvas.Tasks.Actions {

	public class GoToTargetA : ActionTask {

        public BBParameter<NavMeshAgent> navAgent;
        public BBParameter<Transform> destinationA;

        private float recordInterval = 0.5f;
        private float lastRecordTime;

        public static List<Vector3> visitedPositions = new List<Vector3>();

        protected override string OnInit()
        {
            return null;
        }

        protected override void OnExecute()
        {
            if (navAgent.value != null && destinationA.value != null)
            {
                navAgent.value.isStopped = false;
                navAgent.value.SetDestination(destinationA.value.position);

                visitedPositions.Clear();
                lastRecordTime = Time.time;
            }
            else
            {
                EndAction(false);
            }
        }

        protected override void OnUpdate()
        {
            if (navAgent.value == null || !navAgent.value.isOnNavMesh) return;

            //save move log
            if (Time.time - lastRecordTime >= recordInterval)
            {
                visitedPositions.Add(navAgent.value.transform.position);
                lastRecordTime = Time.time;
            }

            if (!navAgent.value.pathPending && navAgent.value.remainingDistance <= navAgent.value.stoppingDistance && !navAgent.value.hasPath)
            {
                EndAction(true);
            }
        }

        protected override void OnStop()
        {
            if (navAgent.value != null && navAgent.value.hasPath)
            {
                navAgent.value.ResetPath();
            }
        }
    }
}