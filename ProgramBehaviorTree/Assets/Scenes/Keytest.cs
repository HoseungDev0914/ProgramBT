using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Keytest : MonoBehaviour
{
    public NavMeshAgent agent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Vector3 testPos = new Vector3(5, 0, 5);
            Debug.Log("Trying to move to: " + testPos);

            bool result = agent.SetDestination(testPos);
            Debug.Log("SetDestination success: " + result);
        }
    }
}