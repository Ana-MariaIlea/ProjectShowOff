using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerAnimationStates
{
    FLYIDLE,
    FLYFORWARD,
    STANDIDLE,
    WALK,
    PUSHBACK
}
public class PlayerAnimationState : MonoBehaviour
{
    [SerializeField]
    Animator animator; 

    public void ChangeAnimatorState(PlayerAnimationStates newState)
    {
        switch (newState)
        {
            case PlayerAnimationStates.FLYIDLE:
                if (animator.GetInteger("condition") == 3 || animator.GetInteger("condition") == 2)
                {
                    //play sound takeoff
                    Debug.Log("PLay sound takeoff");
                    EventQueue.eventQueue.AddEvent(new PlayBeeTakeOffSoundEventData());
                }
                animator.SetInteger("condition", 0);
                break;
            case PlayerAnimationStates.FLYFORWARD:
                if (animator.GetInteger("condition") == 3 || animator.GetInteger("condition") == 2)
                {
                    //play sound takeoff
                    Debug.Log("PLay sound takeoff");

                    EventQueue.eventQueue.AddEvent(new PlayBeeTakeOffSoundEventData());
                }
                animator.SetInteger("condition", 1);
                break;
            case PlayerAnimationStates.STANDIDLE:
                if(animator.GetInteger("condition") == 1|| animator.GetInteger("condition") == 0)
                {
                    //play sound landing
                    Debug.Log("PLay sound landing");

                    EventQueue.eventQueue.AddEvent(new PlayBeeLandingSoundEventData());
                }
                animator.SetInteger("condition", 2);
                break;
            case PlayerAnimationStates.WALK:
                animator.SetInteger("condition", 3);
                break;
            case PlayerAnimationStates.PUSHBACK:
                animator.SetInteger("condition", 4);
                break;
        }
    }
}
