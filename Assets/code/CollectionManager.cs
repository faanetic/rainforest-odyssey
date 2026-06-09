using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager Instance;

    // List untuk menyimpan item yang sudah didapat (tanpa duplikat)
    public List<ItemData> myCollection = new List<ItemData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Agar data tidak hilang saat pindah scene
        }
        else { Destroy(gameObject); }
    }

    public void AddToCollection(ItemData newItem)
    {
        // Cek apakah item sudah ada di koleksi
        if (!myCollection.Contains(newItem))
        {
            myCollection.Add(newItem);
            Debug.Log("Berhasil menambah item baru: " + newItem.itemName);
        }
        else
        {
            Debug.Log("Item sudah ada di koleksi (duplikat diabaikan).");
        }
    }
}