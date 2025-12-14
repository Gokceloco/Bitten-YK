using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isBouncing;

    public float speed;
    public float range;

    private Vector3 _startPos;

    private Vector3 _direction;

    private Weapon _weapon;

    public void StartBullet(Weapon weapon)
    {
        _startPos = transform.position;
        _direction = transform.forward;
        _weapon = weapon;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isBouncing)
            {
                _direction = Vector3.Reflect(_direction, collision.contacts[0].normal);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _weapon.gameDirector.fxManager.PlayZombieImpactPS(transform.position, _direction);
            Destroy(gameObject);
            collision.gameObject.GetComponent<Enemy>().GetHit(1);
        }
    }
    private void Update()
    {
        transform.position += _direction * speed * Time.deltaTime;

        var distance = (_startPos - transform.position).magnitude;

        if (distance > range)
        {
            Destroy(gameObject);
        }
    }

    
}
