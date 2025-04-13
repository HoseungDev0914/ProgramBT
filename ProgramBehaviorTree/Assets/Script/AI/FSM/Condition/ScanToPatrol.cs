using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;

public class ScanToPatrol : ConditionTask
{

    public BBParameter<bool> isScanned;
    public BBParameter<bool> isScanComplete;

    protected override bool OnCheck()
    {
        return isScanComplete.value && isScanned.value == false;
    }
}