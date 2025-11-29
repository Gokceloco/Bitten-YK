using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Level _level;

    public int startHealth;
    private int _currentHealth;

    public void StartEnemy(Level level)
    {
        _level = level;
        _currentHealth = startHealth;
    }

    public void GetHit(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
