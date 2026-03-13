
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected EnemyDataSO data;

    protected Transform player;
    protected int currentHealth;
    protected bool isDead;

    // IDamageable 
    public bool IsDead => isDead;

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    // Unity Lifecycle 
    protected virtual void Awake()
    {
        currentHealth = data.maxHealth;
    }

    protected virtual void Start()
    {
      
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    protected virtual void Update()
    {
        if (isDead || player == null) return;

        float distToPlayer = Vector3.Distance(transform.position, player.position);

        if (distToPlayer <= data.attackRange)
        {
            LookAtPlayer();
            TryAttack();
        }
        else if (distToPlayer <= data.sightRange)
        {
            LookAtPlayer();
        }
    }

    //Shared Behavior 
    protected void LookAtPlayer()
    {
        // Smooth rotation
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f; // keep enemy upright
        Quaternion targetRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation, targetRot, Time.deltaTime * 5f);
    }

    // each enemy type defines this
    protected abstract void TryAttack();

    //Death
    protected virtual void Die()
    {
        isDead = true;
        
        GameEvents.EnemyDied(data.scoreValue, transform.position);
        Destroy(gameObject, 0.3f);
    }
}
