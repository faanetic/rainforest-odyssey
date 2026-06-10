using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioSource bgmAudio;
    
    // Tambahan baru: Wadah untuk mengenali komponen Slider
    public Slider volumeSlider; 

    // Fungsi Start akan otomatis dipanggil sekali saat objek ini aktif/game dimulai
    void Start()
    {
        // Membaca volume lagu saat ini, lalu menyesuaikan posisi knop slider
        if (bgmAudio != null && volumeSlider != null)
        {
            volumeSlider.value = Mathf.Sqrt(bgmAudio.volume) * 50f;
        }
    }

    public void AturVolume(float nilaiVolume)
    {
        float volumeNormal = nilaiVolume / 50f; 
        bgmAudio.volume = volumeNormal * volumeNormal; 
    }
}