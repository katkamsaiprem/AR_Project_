using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private BulletPool bulletPool;
    private InputManager inputManager;

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        if (inputManager != null)
        {
            inputManager.OnShootPressed += Shoot;
        }
    }

    void Shoot()
    {
        GameObject bullet = bulletPool.GetBullet();
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Initialize(bulletPool);
    }

    void OnDestroy()
    {
        if (inputManager != null)
        {
            inputManager.OnShootPressed -= Shoot;
        }
    }
}
