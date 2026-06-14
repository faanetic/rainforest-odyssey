using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Chest : MonoBehaviour
{
    [Header("Pengaturan Peti")]
    public int openPrice = 10;
    public List<ItemData> lootTable; 
    public Sprite openedChestSprite; 

    [Header("Efek Animasi Gacha")]
    public GameObject prefabAnimasiItem; 

    private bool isOpened = false;
    private bool isPlayerNearby = false; 

    void Update()
    {
        // Deteksi tombol E hanya jika player di dekat peti dan peti belum terbuka
        if (isPlayerNearby && !isOpened && Keyboard.current.eKey.wasPressedThisFrame)
        {
            OpenChest();
        }
    }

    public void OpenChest()
    {
        // Memeriksa apakah poin cukup dan berhasil dikurangi
        if (PlayerManager.Instance != null && PlayerManager.Instance.SpendPoints(openPrice))
        {
            isOpened = true;
            Debug.Log("Peti Berhasil Terbuka! Koin resmi terpotong.");

            // ========================================================
            // KUNCI PERBAIKAN: Perintahkan Audio berbunyi DI SINI
            // ========================================================
            ChestAudio komponenAudio = GetComponent<ChestAudio>();
            if (komponenAudio != null)
            {
                komponenAudio.MainkanSuaraPeti();
            }
            // ========================================================

            // Logika mengundi item dari loot table
            if (lootTable != null && lootTable.Count > 0)
            {
                int randomIndex = Random.Range(0, lootTable.Count);
                ItemData itemDapat = lootTable[randomIndex];

                // Lahirkan efek visual item melayang ke atas peti
                if (prefabAnimasiItem != null)
                {
                    Vector3 posisiSpawn = transform.position + new Vector3(0, 0.5f, -0.1f);
                    GameObject objekAnimasi = Instantiate(prefabAnimasiItem, posisiSpawn, Quaternion.identity);
                    objekAnimasi.GetComponent<DroppedItemAnimation>().MulaiAnimasi(itemDapat.itemIcon);
                }

                // Masukkan data ke CollectionManager galeri
                if (CollectionManager.Instance != null)
                {
                    CollectionManager.Instance.AddToCollection(itemDapat);
                }
            }

            // Ubah sprite peti menjadi terbuka
            if (openedChestSprite != null)
            {
                GetComponent<SpriteRenderer>().sprite = openedChestSprite;
            }
        }
        else
        {
            // Jika masuk ke sini, artinya poin kurang. Skrip berhenti dan SUARA TIDAK AKAN BERBUNYI
            Debug.Log("Poin kurang! Gacha dibatalkan, suara hantu berhasil dicegah.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sinkronisasi Tag menggunakan huruf "Player" (P besar)
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("Player dekat peti. Tekan 'E' untuk membuka.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player menjauh.");
        }
    }
}