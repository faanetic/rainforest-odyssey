using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    // 1. DIUBAH MENJADI ARRAY: Sekarang kamu bisa memasukkan lebih dari 1 prefab musuh
    [SerializeField] private GameObject[] enemyPrefabs; 
    [SerializeField] private float spawnRate = 3f;    
    [SerializeField] private int maxEnemies = 5; 
    [SerializeField] private string enemyTag = "Enemy"; 

    [Header("Player Detection Settings")]
    [SerializeField] private string playerTag = "player"; 
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
        // Pengecekan keamanan: Jika kamu lupa memasukkan prefab di Inspector, kode tidak akan error
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("EnemySpawner: Belum ada Prefab musuh yang dimasukkan ke dalam Array!");
            return;
        }

        // 2. LOGIKA ACAK: Memilih indeks secara acak dari prefab yang tersedia
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedEnemyPrefab = enemyPrefabs[randomIndex];

        // Memunculkan enemy yang terpilih secara acak di dalam area radius lingkaran spawner
        Vector2 randomPoint = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
        Instantiate(selectedEnemyPrefab, randomPoint, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("player"))
        {
            Debug.Log("LOG: Objek bernama Player VALID! Spawner mulai aktif.");
            isPlayerInRange = true;
            nextSpawnTime = Time.time; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("player"))
        {
            Debug.Log("LOG: Objek bernama Player keluar area. Spawner nonaktif.");
            isPlayerInRange = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}