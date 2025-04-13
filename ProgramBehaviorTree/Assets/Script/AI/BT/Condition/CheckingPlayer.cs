using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class CheckingPlayer : ConditionTask
{

    public float detectionRange = 10f;

    protected override bool OnCheck()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            float dist = Vector3.Distance(agent.transform.position, player.transform.position);
            if (dist <= detectionRange)
            {
                return true;
            }
        }

        return false;
    }
}