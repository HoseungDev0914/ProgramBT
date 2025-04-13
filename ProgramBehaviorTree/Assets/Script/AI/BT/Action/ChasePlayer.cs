using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class ChasePlayer : ActionTask
{

    public BBParameter<NavMeshAgent> navAgent;
    public float stoppingDistance = 2f;

    private GameObject player;

    protected override void OnExecute()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null || navAgent.value == null)
        {
            Debug.LogWarning("player not found");
            EndAction(false);
            return;
        }

        navAgent.value.isStopped = false;
    }

    protected override void OnUpdate()
    {
        if (player == null || navAgent.value == null)
        {
            EndAction(false);
            return;
        }

        // update player pos
        navAgent.value.SetDestination(player.transform.position);

        float dist = Vector3.Distance(agent.transform.position, player.transform.position);
        if (dist <= stoppingDistance)
        {
            EndAction(true); // reach
        }
    }
}