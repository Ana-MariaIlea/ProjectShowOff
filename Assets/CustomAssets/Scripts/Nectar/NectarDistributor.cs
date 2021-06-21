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

    public bool isDistribuitorSelected = false;
    void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
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


    private void OnTriggerStay(Collider other)
    {
        if (this.enabled)
            if (Input.GetButtonDown("NectarKey"))
            {
                if (other.tag == "Player" && cooldownTimer <= 0)
                {
                    EventQueue.eventQueue.AddEvent(new ChangePlayerStateEventData(PlayerStates.QTEEvent));
                    isDistribuitorSelected = true;
                }
            }
    }
    public void OnNectarIsCollected(EventData eventData)
    {
        if (eventData is NectarCollectStartEventData)
        {
            if (isDistribuitorSelected)
            {
                EventQueue.eventQueue.AddEvent(new NectarCollectEndEventData(nectarAmount));
                cooldownTimer = cooldown;
                if (showParticles == true && polen.isPlaying)
                {
                    polen.Stop();
                }
            }
        }

        isDistribuitorSelected = false;
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

    public void OnFlowerIsPicked(EventData eventData)
    {
        if (eventData is PickFlowerEventData)
        {
            PickFlowerEventData e = eventData as PickFlowerEventData;
            if (e.distributor.gameObject == this)
            {
                EventQueue.eventQueue.UnSubscribe(EventType.NECTARCOLLECTSTART, OnNectarIsCollected);
                Destroy(this);
            }
        }
    }

    public int GetNectarAmount()
    {
        return nectarAmount;
    }

    public void DestroyDistribuitor()
    {
        EventQueue.eventQueue.UnSubscribe(EventType.NECTARCOLLECTSTART, OnNectarIsCollected);
        Destroy(this.gameObject);
    }

    public void SetIsDistribuitorSelectes(bool value)
    {
        isDistribuitorSelected = value;
    }

    public bool GetIsDistribuitorSelectes()
    {
        return isDistribuitorSelected;
    }
}
