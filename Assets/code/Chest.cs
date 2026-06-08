using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private int costToOpen = 10;
    [SerializeField] private List<string> itemPool = new List<string> { "Pedang Emas", "Perisai Kayu", "Ramuan Merah", "Topi Sihir" };
    
    private bool isOpened = false;

    // Fungsi yang dipanggil saat player berinteraksi dengan peti
    public void InteractWithChest()
    {
        if (isOpened)
        {
            Debug.Log("Peti ini sudah kosong.");
            return;
        }

        // Cek apakah poin cukup dan potong langsung jika cukup
        if (GameManager.Instance.SpendPoints(costToOpen))
        {
            OpenChest();
        }
        else
        {
            Debug.Log("Poin kamu tidak cukup untuk membuka peti ini!");
        }
    }

    private void OpenChest()
    {
        isOpened = true;

        // Ambil item secara random dari list
        int randomIndex = Random.Range(0, itemPool.Count);
        string gachaItem = itemPool[randomIndex];

        Debug.Log($"Peti terbuka! Kamu mendapatkan: {gachaItem}");

        // Transfer item yang didapat ke sistem koleksi di GameManager
        GameManager.Instance.AddItemToCollection(gachaItem);

        // Opsional: Matikan visual peti atau putar animasi buka peti
        GetComponent<Collider>().enabled = false; // Biar ga bisa diinteraksi lagi
        // Destroy(gameObject); // Atau hancurkan objeknya jika mau langsung hilang
    }

    // Hanya untuk testing di Inspector Unity
    [ContextMenu("Test Open Chest")]
    private void TestOpen()
    {
        InteractWithChest();
    }
}