using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TelegraphReact : MonoBehaviour
{
    public GuardSteering guard;
    public GameObject exclamationMark;

    public void TriggerTelegraph(Vector3 alertPosition)
    {
        StartCoroutine(SurprisedAndSprint(alertPosition));
    }

    private IEnumerator SurprisedAndSprint(Vector3 position)
    {
        if (exclamationMark != null)
            exclamationMark.SetActive(true);

        guard.agent.isStopped = true;
        yield return new WaitForSeconds(0.8f);

        if (exclamationMark != null)
            exclamationMark.SetActive(false);

        guard.agent.isStopped = false;
        guard.SprintTo(position);
    }
}