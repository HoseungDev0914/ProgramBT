using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class PlayerPosToGuard : ActionTask
{
    public BBParameter<Vector3> TargetPos;
    public BBParameter<Vector3> SignalPosition;
    public BBParameter<bool> IsSignalSent;

    protected override void OnExecute()
    {

        //set position value
        SignalPosition.value = TargetPos.value;
        IsSignalSent.value = true;

        //checking all guard
        GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard");
        foreach (GameObject guard in guards)
        {
            Blackboard bb = guard.GetComponent<Blackboard>();
            if (bb != null)
            {
                bb.SetVariableValue("SignalPosition", SignalPosition.value);
                bb.SetVariableValue("IsSignalSent", true);
            }
        }

        Debug.Log("sent");
        EndAction(true);
    }
}