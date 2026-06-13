using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundManager : MonoBehaviour
{
    public AudioSource sfxAudio;
    public AudioClip clickSound;

    [Header("Pengaturan Volume Mandiri")]
    // Kamu bebas membesarkan angka ini di Inspector (misal 2.0 atau 3.0) 
    // tanpa takut terpengaruh oleh Slider BGM
    public float pengerasSuara = 1.0f; 

    void Start()
    {
        // KODE PLAYERPREFS DIHAPUS DARI SINI
        // Sekarang volume tombol sepenuhnya mandiri!

        // Otomatis mendeteksi semua tombol
        Button[] semuaTombol = GetComponentsInChildren<Button>(true);

        foreach (Button tombol in semuaTombol)
        {
            tombol.onClick.AddListener(MainkanSuaraTombol);
        }
    }

    void MainkanSuaraTombol()
    {
        if (sfxAudio != null && clickSound != null)
        {
            // Memutar suara dengan tingkat kekerasan yang kamu atur di Inspector
            sfxAudio.PlayOneShot(clickSound, pengerasSuara);
        }
    }
}