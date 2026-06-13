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
            // Cek pencegahan jika objek ber-tag Enemy ini adalah area Spawner
            if (collision.GetComponent<EnemySpawner>() != null) return; 

            CharacterStats statEnemy = collision.GetComponent<CharacterStats>();
            if (statEnemy != null)
            {
                Vector2 arahDorong = (collision.transform.position - transform.position).normalized;
                statEnemy.TerimaDamage(damagePeluru, arahDorong);
            }

            Destroy(gameObject); // Hancurkan peluru
        }
        // 2. JIKA MENABRAK OBJEK SOLID LAINNYA (Dinding, Batas Map, Obstacle)
        else
        {
            // Pastikan objek solid yang ditabrak BUKAN area trigger (seperti spawner/item pick-up)
            // Peluru hanya akan hancur jika menabrak collider fisik yang keras (Dinding/Dunia)
            if (collision.isTrigger == false)
            {
                Destroy(gameObject); // Peluru hancur menabrak dinding map
            }
        }
    }
}