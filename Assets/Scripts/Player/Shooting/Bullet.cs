using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 10f;
    
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
        transform.position += transform.up * (speed * Time.deltaTime);
        
        // Check lifetime
        if (Time.time - spawnTime >= lifetime)
        {
            pool.ReturnBullet(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Destroy enemy
            pool.ReturnBullet(gameObject); // Return bullet to pool
        }
    }
}
