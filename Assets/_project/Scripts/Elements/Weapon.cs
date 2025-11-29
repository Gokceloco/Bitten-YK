using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Transform muzzleTransform;

    public float attackRate;

    private float _lastShootTime;

    private void Update()
    {
        var timeSinceLastShoot = Time.time - _lastShootTime;
        if (Input.GetMouseButton(0) && timeSinceLastShoot > attackRate)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = muzzleTransform.position;
        newBullet.transform
            .LookAt(muzzleTransform.position + muzzleTransform.forward);
        _lastShootTime = Time.time;
    }
}
