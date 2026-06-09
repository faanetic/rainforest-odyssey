using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Tambahkan ini di paling atas!

public class Chest : MonoBehaviour
{
    [Header("Pengaturan Peti")]
    public int openPrice = 10;
    public List<ItemData> lootTable; 
    public Sprite openedChestSprite; 

    private bool isOpened = false;
    private bool isPlayerNearby = false; 

    private void Update()
    {
        // Menggunakan New Input System untuk mendeteksi tombol E di keyboard
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