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
        // PERBAIKAN: Pastikan peluru HANYA merespon objek dengan Tag "Enemy"
        if (collision.CompareTag("Enemy"))
        {
            CharacterStats statEnemy = collision.GetComponent<CharacterStats>();
            if (statEnemy != null)
            {
                Vector2 arahDorong = (collision.transform.position - transform.position).normalized;
                statEnemy.TerimaDamage(damagePeluru, arahDorong);
            }

            // Peluru hancur karena berhasil mengenai musuh
            Destroy(gameObject);
        }
    }
}