using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class DestroyAction : ActionTask
{
    public BBParameter<GameObject> ScanableObj; // ScanAction target

    public float destroyDelay = 3f;
    private float timer = 0f;

    protected override void OnExecute()
    {
        timer = 0f;

        if (ScanableObj.value != null)
        {
            ScanableObj.value.SendMessage("OnScanDestroySignal", SendMessageOptions.DontRequireReceiver);
        }
    }

    protected override void OnUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= destroyDelay)
        {
            EndAction(true);
        }
    }
}