using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardSteering : MonoBehaviour
{
    public NavMeshAgent agent;

    float patrolSpeed = 1.5f;
    float sprintSpeed = 3.5f;

    //sway
    public float swayAmount = 0.15f;
    public float swaySpeed = 8f;

    private Vector3 initialLocalPos;
    private bool isSprinting = false;

    void Start()
    {
        initialLocalPos = transform.localPosition;
        StartPatrol();
    }

    void Update()
    {
        if (agent.hasPath) ApplySway();
        else ResetPosition();
    }

    void ApplySway()
    {
        if (!isSprinting) return;
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.localPosition = initialLocalPos + new Vector3(sway, 0, 0);
    }

    void ResetPosition()
    {
        transform.localPosition = initialLocalPos;
    }

    public void StartPatrol()
    {
        isSprinting = false;
        agent.speed = patrolSpeed;
    }

    public void SprintTo(Vector3 destination)
    {
        Debug.Log("[Guard] SprintTo called!");
        isSprinting = true;

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent is NULL!");
            return;
        }

        agent.isStopped = false;
        bool result = agent.SetDestination(destination);
        Debug.Log("SetDestination result: " + result);
    }
}