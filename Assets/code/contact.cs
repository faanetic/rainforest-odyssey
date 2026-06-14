using UnityEngine;

public class CombatContact : MonoBehaviour
{
    private CharacterStats statEnemy;

    void Start()
    {
        statEnemy = GetComponent<CharacterStats>();
    }

    // Menggunakan OnCollisionStay2D agar jika player nempel terus dengan enemy, damage tetap masuk berkala
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Cek apakah objek yang ditabrak adalah Player
        if (collision.gameObject.CompareTag("Player"))
        {
            CharacterStats statPlayer = collision.gameObject.GetComponent<CharacterStats>();

            if (statPlayer != null)
            {
                // 1. Hitung arah knockback (Posisi Player dikurangi Posisi Enemy)
                Vector2 arahKnockback = (collision.transform.position - transform.position).normalized;

                // 2. Berikan damage dan dorongan ke Player berdasarkan damage bawaan enemy ini
                statPlayer.TerimaDamage(statEnemy.damageBawaan, arahKnockback);

                // 3. OPSIONAL: Enemy juga sedikit terpental mundur saat menabrak player agar tidak menyatu kaku
                
                // --- KODE TAMBAHAN: MEMUTAR SUARA HIT ---
                // Mencari script PlayerHurtAudio di objek Player yang baru saja ditabrak
                PlayerHurtAudio playerAudio = collision.gameObject.GetComponent<PlayerHurtAudio>();
                if (playerAudio != null)
                {
                    // Memanggil fungsi untuk membunyikan suara rintihan/hit
                    playerAudio.MainkanSuaraTerserang();
                }
            }
        }
    }
}