using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;

public class ScanAction : ActionTask
{
    public BBParameter<int> patrolIndex;
    public BBParameter<bool> isScanned;
    public BBParameter<bool> isScanComplete;
    public BBParameter<Vector3> TargetPos;
    public BBParameter<GameObject> ScanableObj;
    public BBParameter<bool> IsSignalSent;

    public float scanDuration = 2f;
    public float rotationSpeed = 360f;

    public Transform eyeTransform;
    public LineRenderer scanBeam;

    private float timer = 0f;
    private Transform self;
    private bool scanSuccess = false;

    protected override void OnExecute()
    {
        IsSignalSent.value = false;
        timer = 0f;
        scanSuccess = false;
        isScanComplete.value = false;
        ScanableObj.value = null;

        self = agent.transform;

        //patrol checking index
        patrolIndex.value++;
        if (patrolIndex.value >= 8)
        {
            patrolIndex.value = 0;
        }

        // lazor
        if (scanBeam != null && eyeTransform != null)
        {
            scanBeam.enabled = true;
            scanBeam.SetPosition(0, eyeTransform.position);
            scanBeam.SetPosition(1, eyeTransform.position + eyeTransform.forward * 10f);
        }
    }

    protected override void OnUpdate()
    {
        timer += Time.deltaTime;

        // drone rotate
        if (self != null)
        {
            self.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        // lazor
        if (scanBeam != null && eyeTransform != null)
        {
            Vector3 start = eyeTransform.position;
            Vector3 direction = eyeTransform.forward;
            Vector3 end = start + direction * 10f;

            scanBeam.SetPosition(0, start);
            scanBeam.SetPosition(1, end);

            if (Physics.Raycast(start, direction, out RaycastHit hit, 10f))
            {
                if (hit.collider.CompareTag("Target"))
                {
                    scanSuccess = true;

                    // save target pos
                    TargetPos.value = new Vector3(hit.point.x, 0f, hit.point.z);
                    ScanableObj.value = hit.collider.gameObject;
                }
            }
        }

        if (timer >= scanDuration)
        {
            isScanned.value = scanSuccess;
            isScanComplete.value = true;
        }
    }

    protected override void OnStop()
    {
        if (scanBeam != null)
        {
            scanBeam.enabled = false;
        }
    }
}