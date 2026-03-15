using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthHUD : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private Image healthBar;


    [SerializeField] private float lerpSpeed ;

    private PlayerHealth playerHealth;

    void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void OnEnable()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged += OnHealthChanged;
    }

    void OnDisable()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= OnHealthChanged;
    }

    void OnHealthChanged(int current, int max)
    {
        float pct = Mathf.Clamp01((float)current / max);
        // smooth bar
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, pct, lerpSpeed * Time.deltaTime);
        // color from red to green
        healthBar.color = Color.Lerp(Color.red, Color.green, pct);
        // text
        healthText.text = $"Health: {current}%";
        
    }
}
