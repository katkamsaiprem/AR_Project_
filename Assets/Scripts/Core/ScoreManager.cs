
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int CurrentScore { get; private set; }

    void Awake()
    {
        // Singleton
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void OnEnable()  => GameEvents.OnEnemyDied += OnEnemyDied;
    void OnDisable() => GameEvents.OnEnemyDied -= OnEnemyDied;

    private void OnEnemyDied(int scoreValue, Vector3 position)
    {
        CurrentScore += scoreValue;
        Debug.Log($"score: {CurrentScore}");
        GameEvents.ScoreChanged(CurrentScore);   // UIManager listens to this
    }
}
