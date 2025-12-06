using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int startHealth;
    private int _curHealth;
    public void RestartPlayer()
    {
        transform.position = Vector3.zero;
        _curHealth = startHealth;
        gameObject.SetActive(true);
    }

    public void GetHit(int damage)
    {
        _curHealth -= damage;
        print(_curHealth);
        if (_curHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
