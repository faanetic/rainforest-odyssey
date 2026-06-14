using UnityEngine;

public class PlayerAttackAudio : MonoBehaviour
{
    public AudioSource sfxAudio;
    public AudioClip attackSound;

    [Header("Pengaturan Volume Ekstra")]
    public float pengerasSuara = 2.0f; 

    void Start()
    {
        if (sfxAudio != null)
        {
            // Ambil dasar memori volume dari slider Settings secara global
            sfxAudio.volume = PlayerPrefs.GetFloat("VolumeGlobal", 1f);
        }
    }

    // KUNCI REVISI: Fungsi diubah menjadi Public dan Pasif. 
    // Hanya akan berbunyi jika dipanggil oleh skrip PlayerAttack!
    public void MainkanSuaraTembakan()
    {
        if (sfxAudio != null && attackSound != null)
        {
            sfxAudio.PlayOneShot(attackSound, pengerasSuara);
        }
    }
}