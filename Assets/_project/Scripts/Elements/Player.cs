using System;
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
            Die();
        }
    }

    public void GetHit(int damage)
    {
        _curHealth -= damage;
        healthBar.SetFillBar((float) _curHealth / startHealth);
        if (_curHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        gameDirector.LevelFailed();
        gameObject.SetActive(false);
    }
}
