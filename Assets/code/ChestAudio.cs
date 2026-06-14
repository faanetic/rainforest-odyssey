using UnityEngine;

public class ChestAudio : MonoBehaviour
{
    [Header("Pengaturan Audio")]
    public AudioSource sfxAudio;
    public AudioClip openChestSound;
    public float pengerasSuara = 1.0f;

    private bool sudahBerbunyi = false;

    // Fungsi diubah menjadi Public dan dibuat Pasif.
    // Fungsi ini tidak akan berbunyi sendiri, melainkan menunggu perintah dari Chest.cs!
    public void MainkanSuaraPeti()
    {
        if (!sudahBerbunyi && sfxAudio != null && openChestSound != null)
        {
            sudahBerbunyi = true; // Kunci agar suara gacha peti tidak bisa dispam berkali-kali
            sfxAudio.PlayOneShot(openChestSound, pengerasSuara);
        }
    }
}