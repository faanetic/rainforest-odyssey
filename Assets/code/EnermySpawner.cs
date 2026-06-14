using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject[] enemyPrefabs; 
    [SerializeField] private float spawnRate = 3f;    
    [SerializeField] private int maxEnemies = 5; 
    [SerializeField] private string enemyTag = "Enemy"; 

    [Header("Player Detection Settings")]
    // KUNCI REVISI: Pastikan default-nya di script langsung menggunakan "Player" (P Besar)
    [SerializeField] private string playerTag = "Player"; 
    [SerializeField] private float spawnRadius = 5f;      

    private float nextSpawnTime = 0f;
    private bool isPlayerInRange = false; 

    void Update()
    {
        if (isPlayerInRange && Time.time >= nextSpawnTime)
        {
            int currentEnemyCount = GameObject.FindGameObjectsWithTag(enemyTag).Length;

            if (currentEnemyCount < maxEnemies)
            {
                SpawnEnemy();
            }

            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("EnemySpawner: Belum ada Prefab musuh yang dimasukkan ke dalam Array!");
            return;
        }

        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedEnemyPrefab = enemyPrefabs[randomIndex];

        Vector2 randomPoint = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
        Instantiate(selectedEnemyPrefab, randomPoint, Quaternion.identity);
    }

    // KUNCI REVISI: Menggunakan CompareTag("Player") jauh lebih aman dan akurat dibanding mengecek nama teks objek
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            Debug.Log("LOG: Player VALID memasuki area! Spawner mulai aktif.");
            isPlayerInRange = true;
            nextSpawnTime = Time.time; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            Debug.Log("LOG: Player keluar area. Spawner nonaktif.");
            isPlayerInRange = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}