using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Chest : MonoBehaviour
{
    [Header("Pengaturan Peti")]
    public int openPrice = 10;
    public List<ItemData> lootTable; 
    public Sprite openedChestSprite; 

    // ========================================================
    // [KODE BARU] Wadah untuk menampung prefab efek melayang
    // ========================================================
    [Header("Efek Animasi Gacha")]
    public GameObject prefabAnimasiItem; 
    // ========================================================

    private bool isOpened = false;
    private bool isPlayerNearby = false; 

    private void Update()
    {
        if (isPlayerNearby && !isOpened && Keyboard.current.eKey.wasPressedThisFrame)
        {
            OpenChest();
        }
    }

    public void OpenChest()
    {
        if (PlayerManager.Instance != null && PlayerManager.Instance.SpendPoints(openPrice))
        {
            isOpened = true;
            Debug.Log("Peti Berhasil Terbuka!");

            if (lootTable != null && lootTable.Count > 0)
            {
                int randomIndex = Random.Range(0, lootTable.Count);
                ItemData itemDapat = lootTable[randomIndex];
                Debug.Log("Kamu mendapatkan item: " + itemDapat.itemName);

                // ========================================================
                // [KODE BARU] Logika melahirkan animasi item di atas peti
                // ========================================================
                if (prefabAnimasiItem != null)
                {
                    // Tentukan posisi muncul: di atas peti sedikit (Y + 0.5f)
                    // Dan Z: -0.1f agar gambar item berdiri di DEPAN peti (tidak tertutup/di belakang peti)
                    Vector3 posisiSpawn = transform.position + new Vector3(0, 0.5f, -0.1f);

                    // Lahirkan objek animasinya di map
                    GameObject objekAnimasi = Instantiate(prefabAnimasiItem, posisiSpawn, Quaternion.identity);

                    // Perintahkan objek tersebut memasang gambar item terkait lalu mulai bergerak melayang
                    objekAnimasi.GetComponent<DroppedItemAnimation>().MulaiAnimasi(itemDapat.itemIcon);
                }
                // ========================================================

                if (CollectionManager.Instance != null)
                {
                    CollectionManager.Instance.AddToCollection(itemDapat);
                }
            }

            if (openedChestSprite != null)
            {
                GetComponent<SpriteRenderer>().sprite = openedChestSprite;
            }
        }
        else
        {
            Debug.Log("Poin kurang atau PlayerManager belum siap.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            isPlayerNearby = true;
            Debug.Log("Player dekat peti. Tekan 'E' untuk membuka.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player menjauh.");
        }
    }
}