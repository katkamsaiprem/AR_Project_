using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private EnemyBase enemy;
    [SerializeField] private Image fill;
    [SerializeField] private Text hpText;      // optional
    [SerializeField] private float lerpSpeed = 6f;
    [SerializeField] private Camera mainCam;

    private float targetPct = 1f;

    public void SetCamera(Camera cam)
    {
        mainCam = cam;
        var canvas = GetComponent<Canvas>() ?? GetComponentInParent<Canvas>();
        if (canvas != null) canvas.worldCamera = mainCam;
    }

    void Awake()
    {
        if (enemy == null) enemy = GetComponentInParent<EnemyBase>();
        if (mainCam == null) mainCam = Camera.main;
        var canvas = GetComponent<Canvas>() ?? GetComponentInParent<Canvas>();
        if (canvas != null) canvas.worldCamera = mainCam;
    }

    void OnEnable()
    {
        if (enemy != null) enemy.OnHealthChanged += HandleHealthChanged;
        // Initialize color or fill immediately so spawn starts green
        if (enemy != null) HandleHealthChanged(enemy.CurrentHealth, enemy.MaxHealth);
    }

    void OnDisable()
    {
        if (enemy != null) enemy.OnHealthChanged -= HandleHealthChanged;
    }

    void Update()
    {
        if (enemy == null || enemy.IsDead || fill == null) return;
        fill.fillAmount = Mathf.Lerp(fill.fillAmount, targetPct, lerpSpeed * Time.deltaTime);
    }

    private void HandleHealthChanged(int current, int max)
    {
        if (fill == null) return;
        Debug.Log($"Enemy health changed: {current}/{max}");

        max = Mathf.Max(1, max); // avoid div by zero
        targetPct = Mathf.Clamp01((float)current / max);
        fill.color = Color.Lerp(Color.red, Color.green, targetPct);
        if (hpText != null) hpText.text = $"{current}/{max}";
    }
}
