using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using System.Collections.Generic;

namespace NodeCanvas.Tasks.Conditions
{

    public class ScanRoute : ConditionTask
    {
        public BBParameter<Transform> targetStructure;
        public BBParameter<float> detectionRadius = 5f;
        public string detectionTag = "IllegalStructure";

        public BBParameter<Vector3> detectedPosition;

        private bool visualized = false;

        protected override bool OnCheck()
        {
            List<Vector3> path = NodeCanvas.Tasks.Actions.GoToTargetA.visitedPositions;

            //Visualize save log
            if (!visualized)
            {
                foreach (var point in path)
                {
                    GameObject rangeSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    rangeSphere.transform.position = point + Vector3.up * 0.1f;
                    float diameter = detectionRadius.value * 2f;
                    rangeSphere.transform.localScale = new Vector3(diameter, diameter, diameter);

                    Renderer rend = rangeSphere.GetComponent<Renderer>();
                    rend.material = new Material(Shader.Find("Standard"));
                    rend.material.color = new Color(1f, 0.5f, 0f, 0.1f); // (R,G,B,A)

                    GameObject.Destroy(rangeSphere.GetComponent<Collider>());

                    GameObject.Destroy(rangeSphere, 2f);
                }
            }

            //Scan
            foreach (var point in path)
            {
                Collider[] hits = Physics.OverlapSphere(point, detectionRadius.value);

                foreach (var hit in hits)
                {
                    if (hit.CompareTag(detectionTag))
                    {
                        targetStructure.value = hit.transform;
                        detectedPosition.value = hit.transform.position;

                        Debug.Log("detected");
                        return true;
                    }
                }
            }

            return false;
        }
    }
}