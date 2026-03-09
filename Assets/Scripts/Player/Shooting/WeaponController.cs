using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private int maxAmmo = 10;
   // [SerializeField] private int reloadTime = 1;

   [SerializeField] private ParticleSystem shootEffect;

    private int currentAmmo = 0;
    private bool isReloading = false;
    private InputManager inputManager;

    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        if (inputManager != null)
        {
            Debug.Log("InputManager found and subscribed!");
            inputManager.OnShootPressed += Shoot;
            inputManager.OnReloadPressed += StartReload;
        }
        else
        {
            Debug.LogError("InputManager NOT found!"); 
        }

        currentAmmo = maxAmmo;
    }

    void Shoot()
    {
        if (currentAmmo <= 0 || isReloading)
        {
            return;
        }
        GameObject bullet = bulletPool.GetBullet();
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Initialize(bulletPool);
        

        currentAmmo--;
        Debug.Log($"Ammo: {currentAmmo}/ {maxAmmo}");
       // shootEffect.Play();
    }

    void OnDestroy()//this method is invoked ,When a component or its parent GameObject is explicitly destroyed with Object.Destroy.
  //  When a scene ends
    {
        if (inputManager != null)
        {
            //Memory Leak: The destroyed object remains in memory indefinitely 
            
            inputManager.OnShootPressed -= Shoot;
            inputManager.OnReloadPressed -= StartReload;//we need to unsub to prevent memory leak it means the publisher holds refer to sub even ,sub is no long there
        }
        CancelInvoke(nameof(FinishReload));
    }

    private void StartReload()
    {
        Debug.Log("StartReload called!");
        if (currentAmmo == maxAmmo || isReloading)//we shouldn't reload if we have full ammo or are already reloading
        {
            return;
        }

        isReloading = true;
        Debug.Log("Reloading...");
        //nameof(FinishReload) produces the string name of the method "FinishReload" at compile time
        Invoke(nameof(FinishReload), 2.0f);// using nameof instead of "FinishReload" is safer,If you rename FinishReload, the compiler updates nameof(FinishReload) automatically 

    }

    private void FinishReload()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Reload finished!");
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
