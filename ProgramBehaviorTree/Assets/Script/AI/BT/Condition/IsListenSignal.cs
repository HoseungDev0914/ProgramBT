using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class IsListenSignal : ConditionTask
{
    public BBParameter<Vector3> SignalPosition;
    public BBParameter<bool> IsSignalSent;
    public BBParameter<bool> isAlerted;
    public float maxSignalRange = 50f;

    protected override bool OnCheck()
    {
        if (!IsSignalSent.value)
            return false;

        GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard");
        GameObject closestGuard = null;
        float closestDist = Mathf.Infinity;

        foreach (GameObject guard in guards)
        {
            float dist = Vector3.Distance(SignalPosition.value, guard.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestGuard = guard;
            }
        }

        if (closestGuard == agent.gameObject && closestDist <= maxSignalRange)
        {
            isAlerted.value = true;
            return true;
        }

        return false;
    }
}