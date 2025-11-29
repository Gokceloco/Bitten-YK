using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isBouncing;

    public float speed;
    public float range;

    private Vector3 _startPos;

    private Vector3 _direction;

    private void Start()
    {
        _startPos = transform.position;
        _direction = transform.forward;
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
