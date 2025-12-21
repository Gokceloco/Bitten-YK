using DG.Tweening;
using UnityEditor.U2D;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameDirector gameDirector;
    public Bullet bulletPrefab;
    public Transform muzzleTransform;

    public float machinegunAttackRate;
    public float shotgunAttackRate;

    public float shotgunSpread;

    private float _lastShootTime;

    public ParticleSystem muzzlePS;
    public Light muzzleLight;

    public WeaponType weaponType;

    public GameObject machinegunGameObject;
    public GameObject shotgunGameObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && weaponType != WeaponType.Machinegun)
        {
            weaponType = WeaponType.Machinegun;
            machinegunGameObject.SetActive(true);
            shotgunGameObject.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && weaponType != WeaponType.Shotgun)
        {
            weaponType = WeaponType.Shotgun;
            machinegunGameObject.SetActive(false);
            shotgunGameObject.SetActive(true);
        }

        var timeSinceLastShoot = Time.time - _lastShootTime;
        
        if (weaponType == WeaponType.Machinegun && 
            Input.GetMouseButton(0) && timeSinceLastShoot > machinegunAttackRate
            && gameDirector.gameState == GameState.GamePlay)
        {
            Shoot(0);
            gameDirector.audioManager.PlayShootAS();
        }
        if (weaponType == WeaponType.Shotgun && Input.GetMouseButtonUp(0) 
            && timeSinceLastShoot > shotgunAttackRate && gameDirector.gameState == GameState.GamePlay)
        {
            ShootForShotgun();
            gameDirector.audioManager.PlayShotgunAS();
        }
    }

    private void ShootForShotgun()
    {
        for (int i = 0; i < 20; i++)
        {
            Shoot(shotgunSpread);
        }
    }

    private void Shoot(float spread)
    {
        var newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = muzzleTransform.position;        
        newBullet.transform
            .LookAt(muzzleTransform.position + muzzleTransform.forward 
            + transform.right * Random.Range(-spread, spread)
            + Vector3.up * Random.Range(-spread, spread));
        newBullet.StartBullet(this);
        _lastShootTime = Time.time;
        muzzlePS.Play();

        muzzleLight.DOKill();
        muzzleLight.intensity = 0;
        muzzleLight.DOIntensity(50,.05f).SetLoops(2, LoopType.Yoyo);
        
    }
}

public enum WeaponType
{
    Machinegun,
    Shotgun,
}