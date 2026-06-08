using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f; 

    private Transform playerTransform;
    private SpriteRenderer spriteRenderer; 
    private Rigidbody2D rb; 
    private CharacterStats stats; // Buat variabel cache agar performa lebih ringan

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>(); // Ambil komponen stats musuh
        
        if (rb != null) rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        GameObject playerObject = GameObject.Find("player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() 
    {
        if (playerTransform != null && rb != null)
        {
            // --- PERBAIKAN DI SINI: ---
            // Cek apakah script CharacterStats ada di musuh ini
            if (stats != null)
            {
                // Jika script movement dinonaktifkan (karena sedang knockback), 
                // langsung keluar dari FixedUpdate dan jangan paksa musuh jalan maju!
                if (!this.enabled) return; 
            }
            
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;

        // LOGIKA FLIP
        if (direction.x > 0.01f) 
        {
            spriteRenderer.flipX = true;
        }
        else if (direction.x < -0.01f) 
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnDisable()
    {
        if (rb != null)
        {
            // Saat script dimatikan oleh efek knockback, rem total kecepatan majunya
            rb.linearVelocity = Vector2.zero;
        }
    }
}