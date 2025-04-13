using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class SendingSigAction : ActionTask
{

    public float signalDuration = 2f;
    public float shakeIntensity = 0.2f;
    public float shakeSpeed = 20f;

    public AudioSource signalAudio;

    private float timer = 0f;
    private Vector3 originalPosition;

    public BBParameter<bool> IsSignalSent;

    protected override void OnExecute()
    {
        timer = 0f;
        originalPosition = agent.transform.position;
        IsSignalSent.value = false;

        if (signalAudio != null)
        {
            signalAudio.Play();
        }
    }

    protected override void OnUpdate()
    {
        timer += Time.deltaTime;

        //shake
        float offset = Mathf.Sin(Time.time * shakeSpeed) * shakeIntensity;
        Vector3 shakePos = originalPosition + agent.transform.right * offset;
        agent.transform.position = shakePos;

        if (timer >= signalDuration)
        {
            //reset pos to origin
            agent.transform.position = originalPosition;

            if (signalAudio != null)
            {
                signalAudio.Stop();
            }

            EndAction(true);
        }
    }

    protected override void OnStop()
    {
        agent.transform.position = originalPosition;
        if (signalAudio != null && signalAudio.isPlaying)
        {
            signalAudio.Stop();
        }
    }
}