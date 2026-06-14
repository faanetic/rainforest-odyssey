using UnityEngine;
using UnityEngine.UI;

// Memastikan objek ini otomatis memiliki komponen Image dan Button agar bisa diklik
[RequireComponent(typeof(Image))]
public class GallerySlot : MonoBehaviour
{
    [HideInInspector] public ItemData dataItemIni; // Diisi otomatis oleh GalleryUI saat game berjalan
    private Button tombolSlot;
    private GalleryUI pengontrolGaleri;

    private void Awake()
    {
        pengontrolGaleri = FindObjectOfType<GalleryUI>();

        // Cek apakah sudah ada komponen Button, jika belum ada kita pasang otomatis lewat kode!
        tombolSlot = GetComponent<Button>();
        if (tombolSlot == null)
        {
            tombolSlot = gameObject.AddComponent<Button>();
        }

        // Daftarkan fungsi klik tombol secara otomatis
        tombolSlot.onClick.AddListener(SaatSlotDiklik);
    }

    private void SaatSlotDiklik()
    {
        // Ketika item ini ditekan/diklik, perintahkan Galeri Utama untuk menampilkan deskripsinya
        if (pengontrolGaleri != null && dataItemIni != null)
        {
            pengontrolGaleri.TampilkanTeksPapan(dataItemIni);
        }
    }
}