using UnityEngine;
using UnityEngine.InputSystem; // Wajib untuk mendeteksi kursor dan klik mouse

public class PlayerAttack : MonoBehaviour
{
    [Header("Sistem Menembak")]
    public GameObject prefabPeluru;
    public Transform titikTembak; 
    public float kecepatanPeluru = 12f;

    [Header("Pengaturan Jeda (Cooldown)")]
    public float jedaTembakan = 0.5f; 
    private float waktuTembakBerikutnya = 0f;

    // Tambahkan referensi Audio jika kamu menggabung suara di sini
    [Header("Audio Tembakan (Opsional)")]
    public AudioSource sfxAudio;
    public AudioClip attackSound;
    public float pengerasSuara = 1.0f;

    private movement scriptGerak; 
    private Camera mainCamera;
    private Collider2D playerCollider; // Cache collider player

    void Start()
    {
        scriptGerak = GetComponent<movement>();
        mainCamera = Camera.main;

        // Ambil komponen collider player di awal agar performa lebih ringan
        playerCollider = GetComponent<Collider2D>();

        if (sfxAudio != null)
        {
            sfxAudio.volume = PlayerPrefs.GetFloat("VolumeGlobal", 1f);
        }
    }

    void Update()
    {
        // Deteksi Klik Kiri Mouse lewat Input System baru + cek cooldown
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && Time.time >= waktuTembakBerikutnya)
        {
            Tembak();
            waktuTembakBerikutnya = Time.time + jedaTembakan;
        }
    }

    void Tembak()
    {
        // Jika player sedang terkena knockback (script movement mati), batalkan tembakan
        if (scriptGerak != null && !scriptGerak.enabled) return;

        if (prefabPeluru == null || mainCamera == null) return;

        // Mainkan suara jika dipasang di sini
        if (sfxAudio != null && attackSound != null)
        {
            sfxAudio.PlayOneShot(attackSound, pengerasSuara);
        }

        // 1. Ambil posisi kursor mouse di layar (dalam piksel)
        Vector2 posisiMouseLayar = Mouse.current.position.ReadValue();

        // 2. Konversi posisi piksel layar menjadi koordinat dunia 2D game
        Vector3 posisiMouseDunia = mainCamera.ScreenToWorldPoint(new Vector3(posisiMouseLayar.x, posisiMouseLayar.y, transform.position.z));

        // 3. Tentukan titik kemunculan peluru
        Transform tempatMuncul = titikTembak != null ? titikTembak : transform;

        // 4. Hitung arah dari titik tembak menuju posisi kursor mouse
        Vector2 arahTembak = ((Vector2)posisiMouseDunia - (Vector2)tempatMuncul.position).normalized;

        // 5. Hitung sudut rotasi agar sprite peluru menghadap ke arah kursor
        float sudutRotasi = Mathf.Atan2(arahTembak.y, arahTembak.x) * Mathf.Rad2Deg;

        // 6. Munculkan peluru dengan rotasi yang sudah disesuaikan menuju kursor
        GameObject peluruBaru = Instantiate(prefabPeluru, tempatMuncul.position, Quaternion.Euler(0, 0, sudutRotasi));

        // --- KUNCI PERBAIKAN MASALAH POJOK KIRI BAWAH ---
        // Kita paksa Unity mengabaikan tabrakan fisik antara collider Peluru yang baru lahir
        // dengan collider tubuh Player utama di frame ini.
        if (playerCollider != null)
        {
            Collider2D peluruCollider = peluruBaru.GetComponent<Collider2D>();
            if (peluruCollider != null)
            {
                Physics2D.IgnoreCollision(playerCollider, peluruCollider);
            }
        }
        // ------------------------------------------------

        // 7. Berikan kecepatan gerak pada peluru sesuai arah kursor
        Rigidbody2D rbPeluru = peluruBaru.GetComponent<Rigidbody2D>();
        if (rbPeluru != null)
        {
            rbPeluru.linearVelocity = arahTembak * kecepatanPeluru;
        }
    }
}