
using UnityEngine;

public class EnemyRusher : EnemyBase
{
    private float lastDamageTime;

    protected override void TryAttack()
    {
        // Rushers deal melee damage when close
        if (Time.time - lastDamageTime < data.fireRate) return;
        lastDamageTime = Time.time;

        // Find player and damage them directly
        GameObject playerObj = player.gameObject;
        playerObj.GetComponent<IDamageable>()?.TakeDamage(data.damage);
    }
}
