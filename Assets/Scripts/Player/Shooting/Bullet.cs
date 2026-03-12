using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 10f;
   [SerializeField] private GameObject bulletCollideEffect;
   [SerializeField] private int damage = 20;
    
    private BulletPool pool;
    private float spawnTime;

    public void Initialize(BulletPool bulletPool)
    {
        pool = bulletPool;
        spawnTime = Time.time;//resets timer each time the bullet is spawned
    }

    void Update()
    {
        // Move forward
        transform.position += transform.forward * (speed * Time.deltaTime);
        
        // Check lifetime
        if (Time.time - spawnTime >= lifetime)
        {
            pool.ReturnBullet(gameObject);
        }
    }

void OnTriggerEnter(Collider other)
{
    IDamageable damageable = other.GetComponent<IDamageable>();
    if (damageable != null)
    {
       // damageable.TakeDamage(damage);
        Instantiate(bulletCollideEffect, transform.position, Quaternion.identity);
        pool.ReturnBullet(gameObject);
    }
}

}
