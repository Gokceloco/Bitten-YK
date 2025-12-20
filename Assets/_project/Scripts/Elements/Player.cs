using System;
using UnityEditor.Build.Content;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameDirector gameDirector;
    public PlayerAnimator playerAnimator;

    public int startHealth;
    private int _curHealth;

    public HealthBar healthBar;

    public bool isDead;
    public void RestartPlayer()
    {
        transform.position = Vector3.zero;
        _curHealth = startHealth;
        gameObject.SetActive(true);
        isDead = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion"))
        {
            other.gameObject.SetActive(false);
            gameDirector.LevelCompleted();
            playerAnimator.ChangeAnimationState("Drink");
            gameDirector.fxManager.PlayPotionCollectedPS(other.transform.position);
        }
    }

    private void Update()
    {
        if (transform.position.y < -15f && !isDead)
        {
            Die(0);
        }
    }

    public void GetHit(int damage)
    {
        _curHealth -= damage;
        healthBar.SetFillBar((float) _curHealth / startHealth);
        gameDirector.audioManager.PlayImpactAS();
        if (_curHealth <= 0 && !isDead)
        {
            Die(2);
        }
    }

    private void Die(float uiDelay)
    {
        isDead = true;
        gameDirector.LevelFailed(uiDelay);
        playerAnimator.ChangeAnimationState("Die");
    }
}
