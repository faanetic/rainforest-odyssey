using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damagePeluru = 25f;
    public float durasiHidup = 3f; 

    void Start()
    {
        Destroy(gameObject, durasiHidup);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. JIKA MENABRAK MUSUH
        if (collision.CompareTag("Enemy"))
        {
            // Pengaman khusus agar tidak meledak di area deteksi Spawner
            if (collision.GetComponent<EnemySpawner>() != null) return; 

            CharacterStats statEnemy = collision.GetComponent<CharacterStats>();
            if (statEnemy != null)
            {
                Vector2 arahDorong = (collision.transform.position - transform.position).normalized;
                statEnemy.TerimaDamage(damagePeluru, arahDorong);
            }

            Destroy(gameObject); 
        }
        // 2. KUNCI REVISI: JIKA MENABRAK OBJEK SOLID LINGKUNGAN (Tilemap Dinding Hutan, Batas Map)
        else
        {
            // Peluru hanya akan hancur jika menabrak collider fisik keras (Bukan area trigger spawner/item)
            if (collision.isTrigger == false)
            {
                Destroy(gameObject); 
            }
        }
    }
}