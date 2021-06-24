using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HumanAnimationStates
{
    IDLE,
    WALK,
    SPRAY,
    WALKMOWER,
    PICKFLOWER
}
public class HumanAnimatorState : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public void ChangeAnimatorState(HumanAnimationStates newState)
    {
        switch (newState)
        {
            case HumanAnimationStates.IDLE:
                animator.SetInteger("condition", 0);
                break;
            case HumanAnimationStates.WALK:
                animator.SetInteger("condition", 1);
                break;
            case HumanAnimationStates.SPRAY:
                animator.SetInteger("condition", 2);
                break;
            case HumanAnimationStates.WALKMOWER:
                animator.SetInteger("condition", 3);
                break;
            case HumanAnimationStates.PICKFLOWER:
                animator.SetInteger("condition", 4);
                break;
        }
    }
}
