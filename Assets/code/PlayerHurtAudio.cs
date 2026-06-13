using UnityEngine;

public class PlayerHurtAudio : MonoBehaviour
{
    public AudioSource sfxAudio;
    public AudioClip hurtSound;

    [Header("Pengaturan Volume Ekstra")]
    public float pengerasSuara = 1.5f; // Bisa kamu sesuaikan kekerasannya di Inspector

    void Start()
    {
        if (sfxAudio != null)
        {
            // Menyesuaikan volume dengan slider di menu Settings
            sfxAudio.volume = PlayerPrefs.GetFloat("VolumeGlobal", 1f);
        }
    }

    // FUNGSI UTAMA: Fungsi ini yang akan dipanggil saat darah Player berkurang
    public void MainkanSuaraTerserang()
    {
        if (sfxAudio != null && hurtSound != null)
        {
            sfxAudio.PlayOneShot(hurtSound, pengerasSuara);
        }
    }
}