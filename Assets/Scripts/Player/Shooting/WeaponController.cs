using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private int maxAmmo = 10;
    //[SerializeField] private int reloadTime = 1;
    //[SerializeField] private ParticleSystem shootEffect;
    
    [Header("Muzzle Flash")]
    public GameObject muzzlePrefab;
    public GameObject muzzlePosition;  

    public event System.Action<int, int, bool> OnAmmoChanged; // current, max, isReloading

    private int currentAmmo = 0;
    private bool isReloading = false;
    private InputManager inputManager;

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        if (inputManager != null)
        {
            Debug.Log("InputManager found and subscribed");
            inputManager.OnShootPressed += Shoot;
            inputManager.OnReloadPressed += StartReload;
        }
        else
        {
            Debug.LogError("InputManager not found");
        }

        currentAmmo = maxAmmo;
        OnAmmoChanged?.Invoke(currentAmmo, maxAmmo, isReloading);
    }

    void Shoot()
    {
        if (currentAmmo <= 0 || isReloading)
            return;

        GameObject bullet = bulletPool.GetBullet();
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Initialize(bulletPool);

        currentAmmo--;
        OnAmmoChanged?.Invoke(currentAmmo, maxAmmo, isReloading);
        Debug.Log($"Ammo: {currentAmmo}/ {maxAmmo}");
       // shootEffect?.Play();
       if (muzzlePrefab != null)
       {
      
               var flash = Instantiate(muzzlePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation);
           
           muzzlePrefab.SetActive(true);
       }
    }

    void OnDestroy()
    {
        if (inputManager != null)
        {
            inputManager.OnShootPressed -= Shoot;
            inputManager.OnReloadPressed -= StartReload;
        }
        CancelInvoke(nameof(FinishReload));
    }

    private void StartReload()
    {
        Debug.Log("StartReload called");
        if (currentAmmo == maxAmmo || isReloading)
            return;

        isReloading = true;
        OnAmmoChanged?.Invoke(currentAmmo, maxAmmo, isReloading);
        Debug.Log("Reloading...");
        Invoke(nameof(FinishReload), 2.0f);
    }

    private void FinishReload()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
        OnAmmoChanged?.Invoke(currentAmmo, maxAmmo, isReloading);
        Debug.Log("Reload finished");
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.tKey.wasPressedThisFrame)
        {
            Debug.Log("Manual reload test triggered");
            StartReload();
        }
    }
}


