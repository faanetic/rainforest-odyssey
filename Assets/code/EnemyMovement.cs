using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f; 

    [Header("Anti-Stuck Settings")]
    [SerializeField] private float jarakDeteksiDinding = 0.8f;
    [SerializeField] private LayerMask layerDinding; // Nanti isi dengan layer Tilemap Dinding kamu

    private Transform playerTransform;
    private SpriteRenderer spriteRenderer; 
    private Rigidbody2D rb; 
    private CharacterStats stats; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (rb != null) rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        GameObject playerObject = GameObject.Find("Player");
        if (playerObject == null) playerObject = GameObject.Find("player");

        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
    }

    void FixedUpdate() 
    {
        if (playerTransform != null && rb != null)
        {
            // Jika script dimatikan sementara karena knockback peluru, langsung keluar
            if (!this.enabled) return; 
            
            MoveTowardsPlayerSmart();
        }
    }

    void MoveTowardsPlayerSmart()
    {
        // Arah dasar menuju player
        Vector2 arahKePlayer = (playerTransform.position - transform.position).normalized;
        Vector2 arahJalanFinal = arahKePlayer;

        // Tembakkan garis imajiner (Raycast) ke depan musuh untuk mendeteksi apakah ada dinding menghalangi
        RaycastHit2D hit = Physics2D.Raycast(transform.position, arahKePlayer, jarakDeteksiDinding, layerDinding);
        
        // Garis bantuan visual di jendela Scene (Warna Merah jika tidak ada halangan)
        Debug.DrawRay(transform.position, arahKePlayer * jarakDeteksiDinding, hit.collider != null ? Color.green : Color.red);

        if (hit.collider != null)
        {
            // KUNCI PINTAR TANPA NAVMESH: 
            // Jika di depan ada dinding, paksa arah jalan berbelok 90 derajat (tegak lurus) 
            // ke kiri atau kanan agar musuh melipir di pinggiran dinding dan tidak stuck mematung!
            arahJalanFinal = Vector2.Perpendicular(arahKePlayer); 
        }

        // Terapkan kecepatan ke Rigidbody
        rb.linearVelocity = arahJalanFinal * moveSpeed;

        // LOGIKA FLIP SPRITE
        if (arahKePlayer.x > 0.01f) 
        {
            spriteRenderer.flipX = true;
        }
        else if (arahKePlayer.x < -0.01f) 
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnDisable()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}