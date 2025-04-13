using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    private CharacterController controller;
    private Camera mainCam;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCam = Camera.main;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 inputDir = new Vector3(h, 0, v).normalized;

        Vector3 moveDir = Quaternion.Euler(0, mainCam.transform.eulerAngles.y, 0) * inputDir;
        controller.SimpleMove(moveDir * moveSpeed);

        if (moveDir != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }
    }
}