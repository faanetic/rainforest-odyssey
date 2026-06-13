using UnityEngine;
using UnityEngine.InputSystem; 

public class ChestAudio : MonoBehaviour
{
    [Header("Pengaturan Audio")]
    public AudioSource sfxAudio;
    public AudioClip openChestSound;
    public float pengerasSuara = 1.0f;

    // Variabel penanda
    private bool playerDiDekatChest = false;
    private bool sudahBerbunyi = false; // Mencegah suara terputar berkali-kali saat E ditekan terus

    void Start()
    {
        // Menyesuaikan volume dengan Slider Settings
        // if (sfxAudio != null)
        // {
        //     sfxAudio.volume = PlayerPrefs.GetFloat("VolumeGlobal", 1f);
        // }
    }

    void Update()
    {
        // Jika player di dekat peti dan suara belum pernah diputar
        if (playerDiDekatChest && !sudahBerbunyi)
        {
            // Membaca ketukan tombol "E" dari New Input System
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                MainkanSuaraPeti();
            }
        }
    }

    void MainkanSuaraPeti()
    {
        sudahBerbunyi = true; // Kunci agar suaranya hanya berbunyi sekali
        
        if (sfxAudio != null && openChestSound != null)
        {
            sfxAudio.PlayOneShot(openChestSound, pengerasSuara);
        }
    }

    // Mendeteksi Player masuk ke zona peti
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            playerDiDekatChest = true;
        }
    }

    // Mendeteksi Player keluar dari zona peti
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            playerDiDekatChest = false;
        }
    }
}