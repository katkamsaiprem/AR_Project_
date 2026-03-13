
using System;
using UnityEngine;

public static class GameEvents
{
    // Enemy 
    // (int scoreValue, Vector3 position)
    public static event Action<int, Vector3> OnEnemyDied;
    public static void EnemyDied(int score, Vector3 pos) => OnEnemyDied?.Invoke(score, pos);

    // Player 
    public static event Action OnPlayerDied;
    public static void PlayerDied() => OnPlayerDied?.Invoke();

    // Score 
    public static event Action<int> OnScoreChanged;   // new total
    public static void ScoreChanged(int total) => OnScoreChanged?.Invoke(total);
}
