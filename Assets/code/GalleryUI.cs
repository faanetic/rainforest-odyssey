using UnityEngine;
using UnityEngine.UI;
using TMPro; // --- KODE BARU: Wajib diimport agar script mengenali TextMeshPro ---

public class GalleryUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Transform galleryGridContainer; // Objek 'Content' dari Scroll View
    [SerializeField] private GameObject itemSlotPrefab;      // Prefab 'Item_Slot' dari Assets

    [Header("Papan Deskripsi Kanan")]
    // --- KODE BARU: Mengubah tipe dari 'Text' menjadi 'TextMeshProUGUI' ---
    [SerializeField] private TextMeshProUGUI judulItemText;     
    [SerializeField] private TextMeshProUGUI deskripsiItemText; 

    private void Start()
    {
        DisplayGallery();

        // Atur tulisan awal saat papan galeri baru dibuka
        if (judulItemText != null) judulItemText.text = "";
        if (deskripsiItemText != null) deskripsiItemText.text = "Klik pada item yang telah terpajang di panel kiri untuk membaca kisah penjelasannya.";
    }

    public void DisplayGallery()
    {
        if (galleryGridContainer != null)
        {
            for (int i = galleryGridContainer.childCount - 1; i >= 0; i--)
            {
                Destroy(galleryGridContainer.GetChild(i).gameObject);
            }
        }

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

        var unlockedItems = CollectionManager.Instance.myCollection;

        foreach (ItemData item in unlockedItems)
        {
            if (item == null) continue;

            GameObject newSlot = Instantiate(itemSlotPrefab, galleryGridContainer);
            
            GallerySlot slotScript = newSlot.GetComponent<GallerySlot>();
            if (slotScript != null)
            {
                slotScript.dataItemIni = item; 
            }

            Image slotImage = newSlot.GetComponent<Image>();
            if (slotImage != null && item.itemIcon != null)
            {
                slotImage.sprite = item.itemIcon;
            }
            else if (slotImage == null)
            {
                Debug.LogError("Prefab 'Item_Slot' tidak memiliki komponen UI Image!");
            }
        }
    }

    public void TampilkanTeksPapan(ItemData data)
    {
        if (data == null) return;

        if (judulItemText != null) judulItemText.text = data.itemName;
        if (deskripsiItemText != null) deskripsiItemText.text = data.itemDescription; 
    }
}