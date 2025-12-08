using System;
using UnityEngine;

public class Player : MonoBehaviour
{
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

    public void GetHit(int damage)
    {
        _curHealth -= damage;
        healthBar.SetFillBar((float) _curHealth / startHealth);
        if (_curHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
    }
}
