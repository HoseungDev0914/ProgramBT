using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{

    public class DestroyTargetStructure : ActionTask
    {

        public BBParameter<Transform> targetStructure;

        protected override void OnExecute()
        {
            if (targetStructure.value != null)
            {
                Debug.Log("destroyed");

                GameObject.Destroy(targetStructure.value.gameObject);

                targetStructure.value = null;
            }
            else
            {
                Debug.LogWarning("no object");
            }

            EndAction(true);
        }
    }
}