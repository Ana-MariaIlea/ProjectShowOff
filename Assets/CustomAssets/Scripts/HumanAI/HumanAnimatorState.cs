using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HumanAnimationStates
{
    FLYIDLE,
    FLYFORWARD,
    STANDIDLE,
    WALK,
    PUSHBACK
}
public class HumanAnimatorState : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public void ChangeAnimatorState(HumanAnimationStates newState)
    {
        switch (newState)
        {
            case HumanAnimationStates.FLYIDLE:
                if (animator.GetInteger("condition") == 3 || animator.GetInteger("condition") == 2)
                {
                    //play sound takeoff
                    Debug.Log("PLay sound takeoff");
                    EventQueue.eventQueue.AddEvent(new PlayBeeTakeOffSoundEventData());
                }
                animator.SetInteger("condition", 0);
                break;
            case HumanAnimationStates.FLYFORWARD:
                if (animator.GetInteger("condition") == 3 || animator.GetInteger("condition") == 2)
                {
                    //play sound takeoff
                    Debug.Log("PLay sound takeoff");

                    EventQueue.eventQueue.AddEvent(new PlayBeeTakeOffSoundEventData());
                }
                animator.SetInteger("condition", 1);
                break;
            case HumanAnimationStates.STANDIDLE:
                if (animator.GetInteger("condition") == 1 || animator.GetInteger("condition") == 0)
                {
                    //play sound landing
                    Debug.Log("PLay sound landing");

                    EventQueue.eventQueue.AddEvent(new PlayBeeLandingSoundEventData());
                }
                animator.SetInteger("condition", 2);
                break;
            case HumanAnimationStates.WALK:
                animator.SetInteger("condition", 3);
                break;
        }
    }
}
