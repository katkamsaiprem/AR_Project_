using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected EnemyDataSO data;

    protected Transform player;
    protected int currentHealth;
    protected bool isDead;

    public event System.Action<int, int> OnHealthChanged; // current, max

    
    public int CurrentHealth => currentHealth;
    public int MaxHealth => data != null ? data.maxHealth : 0;

    public bool IsDead => isDead;

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        OnHealthChanged?.Invoke(currentHealth, MaxHealth);
        if (currentHealth <= 0) Die();
    }

    protected virtual void Awake()
    {
        currentHealth = MaxHealth;
        OnHealthChanged?.Invoke(currentHealth, MaxHealth);
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

    protected void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;
        Quaternion targetRot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
    }

    protected abstract void TryAttack();

    protected virtual void Die()
    {
        isDead = true;
        GameEvents.EnemyDied(data.scoreValue, transform.position);
        Destroy(gameObject, 0.3f);
    }
}
