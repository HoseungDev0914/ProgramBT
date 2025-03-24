using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Conditions
{

    public class IsReached : ConditionTask
    {

        public BBParameter<NavMeshAgent> navAgent;
        public BBParameter<Vector3> detectedPosition;

        protected override bool OnCheck()
        {
            if (navAgent.value == null) return false;

            float distance = Vector3.Distance(navAgent.value.transform.position, detectedPosition.value);
            float threshold = navAgent.value.stoppingDistance + 0.1f;

            return distance <= threshold;
        }
    }
}