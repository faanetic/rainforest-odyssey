using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Data")]
    public int currentPoints = 0;

    // Menggunakan HashSet agar otomatis menolak item yang sama (tidak double)
    public HashSet<string> itemCollections = new HashSet<string>();

    private void Awake()
    {
        // Singleton pattern agar data tidak hancur saat pindah scene
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

    // Fungsi mentransfer item ke Scene Collection
    public void AddItemToCollection(string itemName)
    {
        if (itemCollections.Contains(itemName))
        {
            Debug.Log($"Item '{itemName}' sudah ada di koleksi (Double). Tidak ditambahkan.");
        }
        else
        {
            itemCollections.Add(itemName);
            Debug.Log($"Item baru berhasil ditambahkan ke Gallery: {itemName}");
        }
    }
}