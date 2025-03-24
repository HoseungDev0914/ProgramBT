using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;

namespace NodeCanvas.Tasks.Actions {

	public class GoToTargetB : ActionTask {

        public BBParameter<NavMeshAgent> navAgent;
        public BBParameter<Transform> destinationB;

        private float recordInterval = 0.5f;
        private float lastRecordTime;

        // Share log from A
        protected override void OnExecute()
        {
            if (navAgent.value != null && destinationB.value != null)
            {
                navAgent.value.isStopped = false;
                navAgent.value.SetDestination(destinationB.value.position);

                // no reset here (Following Sequence A(reset when start) -> B -> Scan)
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

            if (Time.time - lastRecordTime >= recordInterval)
            {
                GoToTargetA.visitedPositions.Add(navAgent.value.transform.position);
                lastRecordTime = Time.time;
            }

            if (!navAgent.value.pathPending &&
                navAgent.value.remainingDistance <= navAgent.value.stoppingDistance &&
                !navAgent.value.hasPath)
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