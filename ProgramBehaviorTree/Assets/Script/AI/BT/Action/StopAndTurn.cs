using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class StopAndTurn : ActionTask
{


    public BBParameter<NavMeshAgent> navAgent;
    public BBParameter<Vector3> SignalPosition;
    public float rotationSpeed = 360f;
    public float waitDuration = 0.5f; 

    private float timer;
    private Transform self;

    protected override void OnExecute()
    {
        self = agent.transform;
        timer = 0f;

        if (navAgent.value != null)
        {
            navAgent.value.isStopped = true;
        }
    }

    protected override void OnUpdate()
    {
        timer += Time.deltaTime;

        Vector3 direction = (SignalPosition.value - self.position).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            self.rotation = Quaternion.RotateTowards(self.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (timer >= waitDuration)
        {
            EndAction(true);
        }
    }
}