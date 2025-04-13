using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanableObject : MonoBehaviour
{

    public GameObject destroyEffectPrefab;

    public void OnScanDestroySignal()
    {
        if (destroyEffectPrefab != null)
        {
            Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);
            GameObject effect = Instantiate(destroyEffectPrefab, transform.position, rotation);
            Destroy(effect, 3f);
        }
        Destroy(gameObject, 3f);
    }
}