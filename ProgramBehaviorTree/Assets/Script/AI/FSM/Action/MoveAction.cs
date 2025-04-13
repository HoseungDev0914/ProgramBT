using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class MoveAction : ActionTask
{

    public BBParameter<NavMeshAgent> navAgent;
    public BBParameter<Vector3> targetPosition;

    protected override void OnExecute()
    {
        if (navAgent.value != null)
        {
            // follow target's xz position
            Vector3 destination = new Vector3(
                targetPosition.value.x,
                navAgent.value.transform.position.y,
                targetPosition.value.z
            );

            navAgent.value.SetDestination(destination);
        }
    }

    protected override void OnUpdate()
    {
        if (navAgent.value == null || !navAgent.value.isOnNavMesh) return;

        if (!navAgent.value.pathPending &&
            navAgent.value.remainingDistance <= navAgent.value.stoppingDistance)
        {
            EndAction(true); // When it works
        }
    }
}