using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drone12 : MonoBehaviour
{
    public TelegraphReact reaction;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
                Vector3 experimentPos = new Vector3(5, 0, 5);
                reaction.TriggerTelegraph(experimentPos);
        }
    }
}