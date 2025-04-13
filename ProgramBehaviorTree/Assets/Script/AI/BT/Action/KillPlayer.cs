using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class KillPlayer : ActionTask
{

    public float killRange = 2f;

    private GameObject player;

    protected override void OnExecute()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogWarning("player not found");
            EndAction(false);
            return;
        }
    }

    protected override void OnUpdate()
    {
        if (player == null)
        {
            EndAction(false);
            return;
        }

        float dist = Vector3.Distance(agent.transform.position, player.transform.position);

        if (dist <= killRange)
        {
            Debug.Log("player killed");
            GameObject.Destroy(player);
            EndAction(true);
        }
    }
}