using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Data Global")]
    public int currentPoints = 0;

    private void Awake()
    {
        // Setup Singleton pattern agar objek tidak hancur saat pindah scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Fungsi menambah poin setelah bunuh monster
    public void AddPoints(int amount)
    {
        currentPoints += amount;
        Debug.Log("Poin bertambah! Poin sekarang: " + currentPoints);
    }

    // Fungsi mengurangi poin saat buka peti
    public bool SpendPoints(int amount)
    {
        if (currentPoints >= amount)
        {
            currentPoints -= amount;
            Debug.Log("Poin berkurang! Sisa poin: " + currentPoints);
            return true;
        }
        return false; // Poin tidak cukup
    }
    
    // NOTE: Fungsi AddItemToCollection(string) yang lama TELAH DIHAPUS TOTAL 
    // karena sistem inventaris sekarang dialihkan ke CollectionManager.cs
}