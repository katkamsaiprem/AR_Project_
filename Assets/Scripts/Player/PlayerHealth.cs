
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public bool IsDead => currentHealth <= 0;

    // Event so UIManager can update HUD without PlayerHealth knowing about UI
    public event System.Action<int, int> OnHealthChanged; // current, max
    public event System.Action OnPlayerDied;

    void Awake() => currentHealth = maxHealth;

    public void TakeDamage(int damage)
    {
        if (IsDead) return;
        currentHealth = Mathf.Max(0, currentHealth - damage);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (IsDead)
        {
            OnPlayerDied?.Invoke();
            GameEvents.PlayerDied();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}
