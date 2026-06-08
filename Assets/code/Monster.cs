using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private int pointReward = 10;
    [SerializeField] private int health = 100;

    // Fungsi ini dipanggil saat monster menerima damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Panggil GameManager untuk menambah poin
        GameManager.Instance.AddPoints(pointReward);
        
        // Hancurkan objek monster dari map
        Destroy(gameObject);
    }

    // Hanya untuk testing di Inspector Unity (Klik kanan komponen -> pilih "Test Kill")
    [ContextMenu("Test Kill")]
    private void TestKill()
    {
        Die();
    }
}