using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardSteering : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform model;

    float patrolSpeed = 1.5f;
    float sprintSpeed = 3.5f;

    public float swayAmount = 0.15f;
    public float swaySpeed = 8f;

    private Vector3 initialLocalPos;
    private bool isSprinting = false;

    void Start()
    {
        if (model == null)
        {
            Debug.LogError("[GuardSteering] Model reference not assigned!");
            return;
        }

        initialLocalPos = model.localPosition;
        StartPatrol();
    }

    void Update()
    {
        if (agent.hasPath) ApplySway();
        else ResetModelPosition();
    }

    void ApplySway()
    {
        if (!isSprinting || model == null) return;

        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        model.localPosition = initialLocalPos + new Vector3(sway, 0, 0);
    }

    void ResetModelPosition()
    {
        if (model != null)
            model.localPosition = initialLocalPos;
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