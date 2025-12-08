using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public EnemyAnimationState enemyAnimationState;
    public Animator animator;

    public void PlayIdleAnimation()
    {
        if (enemyAnimationState != EnemyAnimationState.Idle)
        {
            enemyAnimationState = EnemyAnimationState.Idle;
            animator.SetTrigger("Idle");
        }
    }

    public void PlayWalkAnimation()
    {
        if (enemyAnimationState != EnemyAnimationState.Walk)
        {
            enemyAnimationState = EnemyAnimationState.Walk;
            animator.SetTrigger("Walk");
        }
    }

    public void PlayAttackAnimaiton()
    {
        animator.SetTrigger("Attack");
        enemyAnimationState = EnemyAnimationState.Attack;
    }

    public void PlayDieAnimation()
    {
        animator.SetTrigger("Die");
        enemyAnimationState = EnemyAnimationState.Die;
    }
}

public enum EnemyAnimationState
{
    Idle,
    Walk,
    Attack,
    Die,
}