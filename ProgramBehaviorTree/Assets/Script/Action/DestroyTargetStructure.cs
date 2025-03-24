using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions
{

    public class DestroyTargetStructure : ActionTask
    {
        public BBParameter<Transform> targetStructure;
        public GameObject destroyEffectPrefab;

        protected override void OnExecute()
        {
            if (targetStructure.value != null)
            {
                if (destroyEffectPrefab != null)
                {
                    GameObject effect = GameObject.Instantiate(
                        destroyEffectPrefab,
                        targetStructure.value.position,
                        Quaternion.identity
                    );
                    GameObject.Destroy(effect, 2f);
                }

                agent.GetComponent<MonoBehaviour>().StartCoroutine(DestroyAfterDelay(1f));
            }
            else
            {
                EndAction(true);
            }
        }

        private IEnumerator DestroyAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            if (targetStructure.value != null)
            {
                GameObject.Destroy(targetStructure.value.gameObject);
                targetStructure.value = null;
            }

            EndAction(true);
        }
    }
}