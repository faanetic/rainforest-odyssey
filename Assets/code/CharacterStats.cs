using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour
{
    [Header("Stat Dasar")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float damageBawaan = 10f;

    [Header("Pengaturan Knockback")]
    public float kekuatanKnockback = 8f;
    public float durasiKnockback = 0.2f;

    [Header("Sistem Imun (I-Frames)")]
    public float durasiImun = 1.5f; // Player imun selama 1.5 detik setelah kena hit
    public bool apakahImun = false;

    [Header("Efek Visual (Opsional)")]
    public SpriteRenderer spriteKarakter;
    public float kecepatanKedip = 0.1f;

    private Rigidbody2D rb;
    private movement scriptGerakPlayer; 
    private bool sedangKnockback = false;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        scriptGerakPlayer = GetComponent<movement>();

        // Otomatis cari SpriteRenderer jika lupa dimasukkan di Inspector
        if (spriteKarakter == null) spriteKarakter = GetComponent<SpriteRenderer>();
    }

    public void TerimaDamage(float jumlahDamage, Vector2 arahPukulan)
    {
        // JIKA SEDANG IMUN ATAU KNOCKBACK, TOLAK SEMUA DAMAGE & KNOCKBACK BARU
        if (apakahImun || sedangKnockback) return; 

        currentHealth -= jumlahDamage;
        Debug.Log(gameObject.name + " menerima damage! Sisa HP: " + currentHealth);

        // Jalankan runtunan efek knockback dan masa imun
        StartCoroutine(EfekKnockback(arahPukulan));
        StartCoroutine(MasaInvincibility());

        if (currentHealth <= 0)
        {
            HancurAtauMati();
        }
    }

    private IEnumerator EfekKnockback(Vector2 arahPukulan)
    {
        sedangKnockback = true;

        // 1. Ambil referensi script gerak milik Player maupun Enemy
        movement scriptGerakPlayer = GetComponent<movement>();
        EnemyMovement scriptGerakEnemy = GetComponent<EnemyMovement>();

        // 2. Matikan script gerakan siapa pun yang terkena hit agar tidak melawan physics
        if (scriptGerakPlayer != null) scriptGerakPlayer.enabled = false;
        if (scriptGerakEnemy != null) scriptGerakEnemy.enabled = false;

        // 3. Berikan gaya pentalan mundur
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Reset kecepatan jalan dulu
            rb.AddForce(arahPukulan * kekuatanKnockback, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(durasiKnockback);

        if (rb != null) rb.linearVelocity = Vector2.zero;
        
        // 4. Hidupkan kembali script gerakan setelah durasi pentalan selesai
        if (scriptGerakPlayer != null) scriptGerakPlayer.enabled = true;
        if (scriptGerakEnemy != null) scriptGerakEnemy.enabled = true;

        sedangKnockback = false;
    }

    // COROUTINE BARU: MENANGANI MASA IMUN DAN EFEK KEDIP VISUAL
    private IEnumerator MasaInvincibility()
    {
        apakahImun = true;

        // Hitung berapa kali sprite harus berkedip selama masa imun
        float timer = 0;
        while (timer < durasiImun)
        {
            // Ubah transparansi sprite menjadi setengah (transparan)
            if (spriteKarakter != null)
                spriteKarakter.color = new Color(1f, 1f, 1f, 0.4f);
            
            yield return new WaitForSeconds(kecepatanKedip);

            // Kembalikan ke warna normal (padat)
            if (spriteKarakter != null)
                spriteKarakter.color = new Color(1f, 1f, 1f, 1f);

            yield return new WaitForSeconds(kecepatanKedip);
            timer += (kecepatanKedip * 2);
        }

        // Pastikan di akhir masa imun sprite kembali normal total
        if (spriteKarakter != null)
            spriteKarakter.color = new Color(1f, 1f, 1f, 1f);

        apakahImun = false;
    }

    private void HancurAtauMati()
    {
        Debug.Log(gameObject.name + " telah mati!");
        
        if (gameObject.CompareTag("Player") || gameObject.name.ToLower().Contains("player"))
        {
            // Panggil DeathManager untuk memproses efek ala Dark Souls
            DeathManager deathManager = FindObjectOfType<DeathManager>();
            if (deathManager != null)
            {
                deathManager.ProsesKematianPlayer();
            }
            else
            {
                Debug.LogError("DeathManager tidak ditemukan di Scene! Pastikan GameObject DeathManagerObject sudah dibuat.");
            }
            
            // Sembunyikan visual player, jangan pakai Destroy agar script coroutine kematian tetap berjalan
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = false;

            movement moveScript = GetComponent<movement>();
            if (moveScript != null) moveScript.enabled = false;

            PlayerAttack attackScript = GetComponent<PlayerAttack>();
            if (attackScript != null) attackScript.enabled = false;
        }
        else
        {
            if (PlayerManager.Instance != null)
            {
                PlayerManager.Instance.AddPoints(5); 
            }
            Destroy(gameObject);
        }
    }
}