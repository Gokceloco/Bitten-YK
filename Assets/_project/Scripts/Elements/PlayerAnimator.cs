using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public void ChangeAnimationState(string key)
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Drink", false);
        animator.SetBool("Die", false);
        animator.SetBool(key, true);
    }

    public void SetWalkDirection(float angle)
    {
        animator.SetFloat("NewWalkBlend", angle);
    }
}
