using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Conditions
{

    public class IsReached : ConditionTask
    {
        public BBParameter<NavMeshAgent> navAgent;
        public BBParameter<Transform> targetStructure;

        protected override bool OnCheck()
        {
            if (navAgent.value == null || targetStructure.value == null)
                return false;

            float distance = Vector3.Distance(navAgent.value.transform.position, targetStructure.value.position);
            float threshold = 2f;
            return distance <= threshold;
        }
    }
}