using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusBoxInteraction : MonoBehaviour
{
    public GameObject scanablePrefab;
    public Material deployedMaterial;    
    public KeyCode interactKey = KeyCode.E;

    private bool isPlayerNearby = false;
    private bool hasDeployed = false;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isPlayerNearby && !hasDeployed && Input.GetKeyDown(interactKey))
        {
            DeployScanableObj();
        }
    }

    void DeployScanableObj()
    {
        if (scanablePrefab != null)
        {
            Instantiate(scanablePrefab, transform.position, Quaternion.identity);
        }

        if (rend != null && deployedMaterial != null)
        {
            rend.material = deployedMaterial;
        }

        hasDeployed = true;
        Debug.Log("works");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}