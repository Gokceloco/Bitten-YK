using DG.Tweening;
using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameDirector gameDirector;
    public Bullet bulletPrefab;
    public Transform muzzleTransform;

    public float attackRate;

    private float _lastShootTime;

    public ParticleSystem muzzlePS;
    public Light muzzleLight;

    private void Update()
    {
        var timeSinceLastShoot = Time.time - _lastShootTime;
        if (Input.GetMouseButton(0) && timeSinceLastShoot > attackRate
            && gameDirector.gameState == GameState.GamePlay)
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
        newBullet.StartBullet(this);
        _lastShootTime = Time.time;
        muzzlePS.Play();

        muzzleLight.DOKill();
        muzzleLight.intensity = 0;
        muzzleLight.DOIntensity(50,.05f).SetLoops(2, LoopType.Yoyo);
        gameDirector.audioManager.PlayShootAS();
    }
}
