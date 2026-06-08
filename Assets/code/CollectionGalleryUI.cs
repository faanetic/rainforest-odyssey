using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CollectionGalleryUI : MonoBehaviour
{
    [System.Serializable]
    public struct GalleryItemUI
    {
        public string itemName;
        public GameObject itemImageObject; // Gambar/Slot UI di Gallery
    }

    [Header("Daftar Semua Slot Item di UI Gallery")]
    public List<GalleryItemUI> gallerySlots;

    private void Start()
    {
        UpdateGalleryDisplay();
    }

    public void UpdateGalleryDisplay()
    {
        // Ambil data koleksi yang sudah terkumpul di GameManager
        HashSet<string> playerCollections = GameManager.Instance.itemCollections;

        foreach (var slot in gallerySlots)
        {
            // Jika item sudah pernah didapatkan, aktifkan gambarnya (di-unhide)
            if (playerCollections.Contains(slot.itemName))
            {
                slot.itemImageObject.SetActive(true); 
                // Atau ubah warnanya dari hitam (locked) menjadi berwarna (unlocked)
                // slot.itemImageObject.GetComponent<Image>().color = Color.white;
            }
            else
            {
                // Jika belum dapat, sembunyikan atau buat jadi siluet hitam
                slot.itemImageObject.SetActive(false);
            }
        }
    }
}