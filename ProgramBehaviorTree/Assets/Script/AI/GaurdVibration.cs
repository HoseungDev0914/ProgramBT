using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaurdVibration : MonoBehaviour
{
    public Transform visualModel;
    public float shakeSpeed = 20f;
    public float shakeAmount = 0.05f;

    private Vector3 baseOffset;

    void Start()
    {
        if (visualModel != null)
        {
            baseOffset = visualModel.localPosition;
        }
    }

    void Update()
    {
        if (visualModel != null)
        {
            float offset = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
            Vector3 shake = visualModel.right * offset;
            visualModel.localPosition = baseOffset + shake;
        }
    }
}