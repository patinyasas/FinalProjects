using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs
    public Vector3 spawnAreaSize = new Vector3(10, 0, 10); // Define the size of the spawn area
    public float spawnInterval = 2f; // Interval between enemy spawns (in seconds)
    public int maxEnemies = 5; // Maximum number of enemies to spawn

    private int currentEnemyCount = 0; // Track the current number of enemies spawned

    void Start()
    {
        // Start spawning enemies repeatedly at the specified interval
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval); // Start with no delay, repeat every spawnInterval seconds
    }

    void SpawnEnemy()
    {
        if (currentEnemyCount < maxEnemies) // Only spawn if the number of enemies is less than the max
        {
            Vector3 randomPosition = GetRandomPosition();
            GameObject enemyPrefab = GetRandomEnemyPrefab(); // Get a random enemy prefab from the array
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity); // Spawn the enemy
            currentEnemyCount++; // Increment the enemy count
        }
        else
        {
            CancelInvoke("SpawnEnemy"); // Stop spawning once max enemies are reached
            Debug.Log("Max enemy count reached.");
        }
    }

    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomZ = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);
        return new Vector3(randomX, 0, randomZ); // Assuming Y is 0 for ground level
    }

    GameObject GetRandomEnemyPrefab()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length); // Get a random index in the enemyPrefabs array
        return enemyPrefabs[randomIndex]; // Return the prefab at the random index
    }
}