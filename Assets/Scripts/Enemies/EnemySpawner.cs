
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct EnemySpawnEntry
    {
        public GameObject prefab;       // prefab with EnemyShooter or EnemyRusher
        [Range(0, 100)]
        public int spawnWeight;         // higher = more common
    }

    [SerializeField] private List<EnemySpawnEntry> enemyTypes;
    [SerializeField] private int maxEnemiesAlive = 3;
    [SerializeField] private float initialSpawnRetry = 0.5f; // seconds between attempts while no planes
    [SerializeField] private float respawnDelay = 2f;
    [SerializeField] private ARRaycastManager arRaycastManager;
    [SerializeField] private Camera arCamera;

    private int activeEnemies;
    private readonly List<ARRaycastHit> arHits = new List<ARRaycastHit>();
    private Coroutine initialSpawner;

    void OnEnable()  => GameEvents.OnEnemyDied += OnEnemyDied;
    void OnDisable() => GameEvents.OnEnemyDied -= OnEnemyDied;

    void Start()
    {
        // Start a coroutine that keeps trying until we reach the target count
        initialSpawner = StartCoroutine(SpawnUntilFull());
    }

    private IEnumerator SpawnUntilFull()
    {
        while (activeEnemies < maxEnemiesAlive)
        {
            if (TrySpawnOnce())
            {
                continue; // successfully spawned loop checks if we need more
            }
            // No plane or no prefab wait and try again
            yield return new WaitForSeconds(initialSpawnRetry);
        }
        initialSpawner = null;
    }

    // Called by GameEvents when any enemy dies
    private void OnEnemyDied(int scoreValue, Vector3 position)
    {
        activeEnemies = Mathf.Max(0, activeEnemies - 1);
        if (activeEnemies < maxEnemiesAlive)
            Invoke(nameof(SpawnEnemyOnce), respawnDelay);
    }

    private void SpawnEnemyOnce()
    {
        TrySpawnOnce();
        if (activeEnemies < maxEnemiesAlive && initialSpawner == null)
            initialSpawner = StartCoroutine(SpawnUntilFull()); // we refill if still below max after this spawn
    }

    private bool TrySpawnOnce()
    {
        Vector3 spawnPos = GetRandomARSpawnPoint();
        if (spawnPos == Vector3.zero) return false; 

        GameObject prefab = PickWeightedRandom();
        if (prefab == null) return false;

        Instantiate(prefab, spawnPos, Quaternion.identity);
        activeEnemies++;
        return true;
    }

     
    private Vector3 GetRandomARSpawnPoint()
    {
        if (arRaycastManager == null || arCamera == null) return Vector3.zero;

        Vector2 screenPoint = GetRandomScreenEdgePoint();
        if (arRaycastManager.Raycast(screenPoint, arHits, TrackableType.PlaneWithinPolygon))
        {
            Vector3 pos = arHits[0].pose.position;
            pos += new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            return pos;
        }
        return Vector3.zero;    // AR plane not detected 
    }

    private Vector2 GetRandomScreenEdgePoint()
    {
        int edge = Random.Range(0, 4);
        return edge switch
        {
            0 => new Vector2(Random.Range(0f, Screen.width), 0),             // bottom
            1 => new Vector2(Random.Range(0f, Screen.width), Screen.height), // top
            2 => new Vector2(0, Random.Range(0f, Screen.height)),            // left
            _ => new Vector2(Screen.width, Random.Range(0f, Screen.height)), // right
        };
    }

    // Weighted Random Enemy Picker 
    private GameObject PickWeightedRandom()
    {
        int totalWeight = 0;
        foreach (var entry in enemyTypes) totalWeight += entry.spawnWeight;

        if (totalWeight <= 0) return null;

        int roll = Random.Range(0, totalWeight);
        int cumulative = 0;
        foreach (var entry in enemyTypes)
        {
            cumulative += entry.spawnWeight;
            if (roll < cumulative) return entry.prefab;
        }
        return null;
    }
}
