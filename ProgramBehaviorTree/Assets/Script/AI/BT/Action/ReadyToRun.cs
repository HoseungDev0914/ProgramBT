using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class ReadyToRun : ActionTask
{

    public GameObject exclamationPrefab;
    public float displayTime = 1f;
    public Vector3 offset = new Vector3(0, 10f, 0); // above head

    private GameObject exclamationInstance;
    private float timer = 0f;

    protected override void OnExecute()
    {
        timer = 0f;

        if (exclamationPrefab != null)
        {
            Vector3 spawnPos = agent.transform.position + offset;
            exclamationInstance = GameObject.Instantiate(exclamationPrefab, spawnPos, Quaternion.identity);
            exclamationInstance.transform.SetParent(agent.transform); // attaching on guard
        }
    }

    protected override void OnUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= displayTime)
        {
            if (exclamationInstance != null)
                GameObject.Destroy(exclamationInstance);

            EndAction(true);
        }
    }

    protected override void OnStop()
    {
        if (exclamationInstance != null)
            GameObject.Destroy(exclamationInstance);
    }
}