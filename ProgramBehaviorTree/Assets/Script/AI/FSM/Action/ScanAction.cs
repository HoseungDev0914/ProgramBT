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

    public float scanDuration = 2f;
    public float rotationSpeed = 360f;

    public Transform eyeTransform;
    public LineRenderer scanBeam;

    private float timer = 0f;
    private Transform self;
    private bool scanSuccess = false;

    protected override void OnExecute()
    {
        timer = 0f;
        scanSuccess = false;
        isScanComplete.value = false;
        ScanableObj.value = null;

        self = agent.transform;

        // ✅ 패트롤 인덱스 증가
        patrolIndex.value++;
        if (patrolIndex.value >= 8)
        {
            patrolIndex.value = 0;
        }

        // ✅ 레이저 시각 효과 시작
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

        // ✅ 드론 회전
        if (self != null)
        {
            self.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        // ✅ 레이저 방향 & Raycast
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

                    // ✅ 목표 좌표와 대상 오브젝트 저장
                    TargetPos.value = new Vector3(hit.point.x, 0f, hit.point.z);
                    ScanableObj.value = hit.collider.gameObject;
                }
            }
        }

        // ✅ 스캔 종료 타이밍
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