
using UnityEngine;

public class EnemyShooter : EnemyBase
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private float lastFireTime;

    protected override void TryAttack()
    {
        // data.fireRate comes from ScriptableObject
        if (Time.time - lastFireTime < data.fireRate) return;

        lastFireTime = Time.time;
        ShootAtPlayer();
    }

    private void ShootAtPlayer()
    {
        if (bulletPrefab == null || firePoint == null) return;

        // Aim at player
        Vector3 direction = (player.position - firePoint.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotation);

        // Enemy bullet uses same Bullet.cs but with different tag handling
        EnemyBullet eb = bullet.GetComponent<EnemyBullet>();
        if (eb != null) eb.SetDamage(data.damage);
    }
}
