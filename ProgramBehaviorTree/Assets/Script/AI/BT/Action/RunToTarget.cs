using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class RunToTarget : ActionTask
{

    public BBParameter<NavMeshAgent> navAgent;
    public BBParameter<Vector3> SignalPosition;

    private float originalSpeed;

    protected override void OnExecute()
    {
        if (navAgent.value == null)
        {
            EndAction(false);
            return;
        }

        // run speed
        originalSpeed = navAgent.value.speed;
        navAgent.value.speed = originalSpeed * 2f;

        // lock target
        navAgent.value.isStopped = false;
        navAgent.value.SetDestination(SignalPosition.value);
    }

    protected override void OnUpdate()
    {
        if (navAgent.value.pathPending)
            return;

        if (navAgent.value.remainingDistance <= navAgent.value.stoppingDistance)
        {
            EndAction(true);
        }
    }

    protected override void OnStop()
    {
        // reset to walking speed
        if (navAgent.value != null)
        {
            navAgent.value.speed = originalSpeed;
        }
    }
}