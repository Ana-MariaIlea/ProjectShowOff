using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NectarDistributor : MonoBehaviour
{
    [SerializeField]
    private int nectarAmount;
    [SerializeField]
    private float cooldown;
    [SerializeField]
    private ParticleSystem polen;
    private bool showParticles = false;
    private float cooldownTimer = 0;

    private void Start()
    {
        EventQueue.eventQueue.Subscribe(EventType.NECTARCOLLECTSTART, OnNectarIsCollected);
    }

    private void Update()
    {
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
        else
        {
            if (showParticles == true && !polen.isPlaying)
            {
                polen.Play();
            }
        }
    }

    public void OnNectarIsCollected(EventData eventData)
    {
        if (eventData is NectarCollectStartEventData)
        {
            NectarCollectStartEventData e = eventData as NectarCollectStartEventData;
            if (e.dis == this)
            {
                if (cooldownTimer <= 0)
                {
                    EventQueue.eventQueue.AddEvent(new NectarCollectEndEventData(nectarAmount));
                    cooldownTimer = cooldown;
                    if (showParticles == true && polen.isPlaying)
                    {
                        polen.Stop();
                    }
                }
            }
        }
    }


    public void OnChangeCooldoenTime(EventData eventData)
    {
        if (eventData is ChangeDifficultyEventData)
        {
            ChangeDifficultyEventData e = eventData as ChangeDifficultyEventData;
            cooldown = e.Difficulty.NectarCooldownTime;
            showParticles = e.Difficulty.FlowersHaveParticles;
        }
    }
}
