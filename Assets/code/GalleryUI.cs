using UnityEngine;
using UnityEngine.UI;

public class GalleryUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Transform galleryGridContainer; // Objek 'Content' dari Scroll View
    [SerializeField] private GameObject itemSlotPrefab;      // Prefab 'Item_Slot' dari Assets

    private void Start()
    {
        DisplayGallery();
    }

    public void DisplayGallery()
    {
        // 1. Bersihkan UI lama dengan aman (menggunakan loop mundur agar index tidak kacau)
        if (galleryGridContainer != null)
        {
            for (int i = galleryGridContainer.childCount - 1; i >= 0; i--)
            {
                Destroy(galleryGridContainer.GetChild(i).gameObject);
            }
        }

        // 2. Cek validasi data dasar
        if (CollectionManager.Instance == null)
        {
            Debug.LogWarning("CollectionManager belum ada di scene!");
            return;
        }

        if (itemSlotPrefab == null)
        {
            Debug.LogError("Item Slot Prefab belum ditarik ke Inspector GalleryUI!");
            return;
        }

        // 3. Ambil daftar item yang sudah didapatkan pemain
        var unlockedItems = CollectionManager.Instance.myCollection;

        // 4. Instansiasi objek baru secara bersih
        foreach (ItemData item in unlockedItems)
        {
            if (item == null) continue;

            // Buat slot baru langsung menjadi anak dari container
            GameObject newSlot = Instantiate(itemSlotPrefab, galleryGridContainer);
            
            // Ambil komponen Image dari slot yang BARU SAJA dibuat (bukan objek lama)
            Image slotImage = newSlot.GetComponent<Image>();
            
            if (slotImage != null && item.itemIcon != null)
            {
                slotImage.sprite = item.itemIcon;
            }
            else if (slotImage == null)
            {
                Debug.LogError("Prefab 'Item_Slot' tidak memiliki komponen UI Image! Pastikan Anda tidak salah membuat prefab.");
            }
        }
    }
}