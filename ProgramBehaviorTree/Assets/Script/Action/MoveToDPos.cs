using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions
{

    public class MoveToDPos : ActionTask
    {

        public BBParameter<NavMeshAgent> navAgent;
        public BBParameter<Vector3> detectedPosition;

        protected override void OnExecute()
        {
            if (navAgent.value != null)
            {
                navAgent.value.isStopped = false;
                navAgent.value.SetDestination(detectedPosition.value);
            }
            else
            {
                EndAction(false);
            }
        }

        protected override void OnUpdate()
        {
            if (navAgent.value == null) return;

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