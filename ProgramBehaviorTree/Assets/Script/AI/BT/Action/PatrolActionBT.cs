using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class PatrolActionBT : ActionTask
{

    public BBParameter<NavMeshAgent> navAgent;
    public BBParameter<Transform[]> patrolPoints;
    public BBParameter<int> patrolIndex;

    protected override void OnExecute()
    {
        if (navAgent.value == null || patrolPoints.value == null || patrolPoints.value.Length == 0)
        {
            EndAction(false);
            return;
        }

        int index = patrolIndex.value;
        index = Mathf.Clamp(index, 0, patrolPoints.value.Length - 1);

        Transform target = patrolPoints.value[index];
        navAgent.value.SetDestination(target.position);
    }

    protected override void OnUpdate()
    {
        if (navAgent.value == null || !navAgent.value.isOnNavMesh) return;

        if (!navAgent.value.pathPending && navAgent.value.remainingDistance <= navAgent.value.stoppingDistance)
        {
            patrolIndex.value = (patrolIndex.value + 1) % patrolPoints.value.Length;
            EndAction(true);
        }
    }
}