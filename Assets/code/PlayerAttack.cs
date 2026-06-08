using UnityEngine;
using UnityEngine.InputSystem; // Wajib untuk mendeteksi kursor dan klik mouse

public class PlayerAttack : MonoBehaviour
{
    [Header("Sistem Menembak")]
    public GameObject prefabPeluru;
    public Transform titikTembak; 
    public float kecepatanPeluru = 12f;

    private movement scriptGerak; 
    private Camera mainCamera;

    void Start()
    {
        scriptGerak = GetComponent<movement>();
        // Mengambil referensi kamera utama untuk konversi koordinat posisi mouse
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Deteksi Klik Kiri Mouse lewat Input System baru
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Tembak();
        }
    }

    void Tembak()
    {
        // Jika player sedang terkena knockback (script movement mati), batalkan tembakan
        if (scriptGerak != null && !scriptGerak.enabled) return;

        if (prefabPeluru == null || mainCamera == null) return;

        // 1. Ambil posisi kursor mouse di layar (dalam piksel)
        Vector2 posisiMouseLayar = Mouse.current.position.ReadValue();

        // 2. Konversi posisi piksel layar menjadi koordinat dunia 2D game
        Vector3 posisiMouseDunia = mainCamera.ScreenToWorldPoint(new Vector3(posisiMouseLayar.x, posisiMouseLayar.y, transform.position.z));

        // 3. Tentukan titik kemunculan peluru
        Transform tempatMuncul = titikTembak != null ? titikTembak : transform;

        // 4. Hitung arah dari titik tembak menuju posisi kursor mouse
        Vector2 arahTembak = ((Vector2)posisiMouseDunia - (Vector2)tempatMuncul.position).normalized;

        // 5. Hitung sudut rotasi agar sprite peluru menghadap ke arah kursor (tidak miring kaku)
        float sudutRotasi = Mathf.Atan2(arahTembak.y, arahTembak.x) * Mathf.Rad2Deg;

        // 6. Munculkan peluru dengan rotasi yang sudah disesuaikan menuju kursor
        GameObject peluruBaru = Instantiate(prefabPeluru, tempatMuncul.position, Quaternion.Euler(0, 0, sudutRotasi));

        // 7. Berikan kecepatan gerak pada peluru sesuai arah kursor
        Rigidbody2D rbPeluru = peluruBaru.GetComponent<Rigidbody2D>();
        if (rbPeluru != null)
        {
            rbPeluru.linearVelocity = arahTembak * kecepatanPeluru;
        }
    }
}