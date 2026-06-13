using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerAttackAudio : MonoBehaviour
{
    public AudioSource sfxAudio;
    public AudioClip attackSound;

    [Header("Pengaturan Volume Ekstra")]
    // Angka 2.0f berarti suara serangan ini akan dipaksa 2x lipat lebih keras dari BGM
    public float pengerasSuara = 2.0f; 

    void Start()
    {
        if (sfxAudio != null)
        {
            // Tetap mengambil dasar memori volume dari slider Settings
            sfxAudio.volume = PlayerPrefs.GetFloat("VolumeGlobal", 1f);
        }
    }

    void Update()
    {
        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (sfxAudio != null && attackSound != null)
                {
                    // Triknya di sini: Kita masukkan 'pengerasSuara' sebagai pengali
                    sfxAudio.PlayOneShot(attackSound, pengerasSuara);
                }
            }
        }
    }
}