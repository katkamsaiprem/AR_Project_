
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "AR Shooter/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    [Header("Identity")]
    public string enemyName = "Basic Enemy";
    public int scoreValue = 10;

    [Header("Health")]
    public int maxHealth = 50;

    [Header("Combat")]
    public float fireRate = 2f;        // seconds between shots
    public int damage = 10;
    public float attackRange = 5f;
    public float sightRange = 8f;

    [Header("Movement")]
    public float moveSpeed = 1.5f;     // smooth LookAt speed
}
