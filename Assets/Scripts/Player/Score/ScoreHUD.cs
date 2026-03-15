using UnityEngine;
using UnityEngine.UI;

public class ScoreHUD : MonoBehaviour
{
    [SerializeField] Text scoreText;
    void OnEnable()  => GameEvents.OnScoreChanged += UpdateScore;
    void OnDisable() => GameEvents.OnScoreChanged -= UpdateScore;
    void UpdateScore(int total) => scoreText.text = $"Kill Credits: {total}";
}