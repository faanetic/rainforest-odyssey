using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro; 

public class GalleryUI : MonoBehaviour
{
    [Header("Komponen Grid UI")]
    [SerializeField] private Transform galleryGridContainer; // Objek 'Content' dari Scroll View
    [SerializeField] private GameObject itemSlotPrefab;      // Ujung masalah eror merah kamu

    [Header("Papan Deskripsi Kanan")]
    [SerializeField] private TextMeshProUGUI judulItemText;     
    [SerializeField] private TextMeshProUGUI deskripsiItemText; 

    private void Start()
    {
        // Setelan awal teks papan informasi kanan
        if (judulItemText != null) judulItemText.text = "Galeri Koleksi";
        if (deskripsiItemText != null) deskripsiItemText.text = "Klik pada salah satu item yang telah terpajang di panel kiri untuk membaca kisah penjelasannya.";

        // Minta data ke CollectionManager secara teratur
        if (CollectionManager.Instance != null)
        {
            CollectionManager.Instance.MintaUpdateTampilanGaleri(this);
        }
        else
        {
            Debug.LogWarning("GalleryUI: CollectionManager Instance belum lahir di scene ini!");
        }
    }

    // Fungsi pencetakan slot aman yang dipanggil oleh CollectionManager
    public void EksekusiCetakSlot(List<ItemData> daftarKoleksi)
    {
        // Bersihkan sisa slot lama di grid agar tidak duplikat menumpuk
        if (galleryGridContainer != null)
        {
            for (int i = galleryGridContainer.childCount - 1; i >= 0; i--)
            {
                Destroy(galleryGridContainer.GetChild(i).gameObject);
            }
        }

        if (itemSlotPrefab == null)
        {
            Debug.LogError("GalleryUI: Item Slot Prefab masih kosong di Inspector! Tolong tarik file Prefab UI Slot-mu.");
            return;
        }

        // Mulai cetak otomatis tombol slot berdasarkan data achievement permanen
        foreach (ItemData item in daftarKoleksi)
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
        }
        Debug.Log("GalleryUI: Sukses mencetak " + daftarKoleksi.Count + " item koleksi ke layar Canvas.");
    }

    public void TampilkanTeksPapan(ItemData data)
    {
        if (data == null) return;

        if (judulItemText != null) judulItemText.text = data.itemName;
        if (deskripsiItemText != null) deskripsiItemText.text = data.itemDescription; 
    }
}