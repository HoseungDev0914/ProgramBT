using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;

public class PlayerDetected : ConditionTask
{
    public BBParameter<Vector3> playerLastKnownPosition;
    public BBParameter<bool> IsRecentlyDetected;
    public float detectionRadius = 15f;

    protected override bool OnCheck()
    {
        if (IsRecentlyDetected.value)
            return false;

        Collider[] hits = Physics.OverlapSphere(agent.transform.position, detectionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                playerLastKnownPosition.value = hit.transform.position;
                IsRecentlyDetected.value = true; // detection check
                return true;
            }
        }

        return false;
    }
}