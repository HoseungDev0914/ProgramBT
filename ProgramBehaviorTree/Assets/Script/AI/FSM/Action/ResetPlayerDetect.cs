using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class ResetPlayerDetect : ActionTask
{

    public BBParameter<bool> IsRecentlyDetected;

    protected override void OnExecute()
    {
        IsRecentlyDetected.value = false;
        EndAction(true);
    }
}