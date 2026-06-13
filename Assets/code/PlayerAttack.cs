using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Sistem Menembak")]
    public GameObject prefabPeluru;
    public Transform titikTembak; 
    public float kecepatanPeluru = 12f;

    [Header("Pengaturan Jeda (Cooldown)")]
    public float jedaTembakan = 0.5f; // Player hanya bisa menembak setiap 0.5 detik sekali
    private float waktuTembakBerikutnya = 0f;

    private movement scriptGerak; 
    private Camera mainCamera;

    void Start()
    {
        scriptGerak = GetComponent<movement>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Deteksi Klik Kiri Mouse + periksa apakah waktu tunggu cooldown sudah selesai
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && Time.time >= waktuTembakBerikutnya)
        {
            Tembak();
            // Perbarui waktu kapan player boleh menembak lagi di frame masa depan
            waktuTembakBerikutnya = Time.time + jedaTembakan;
        }
    }

    void Tembak()
    {
        if (scriptGerak != null && !scriptGerak.enabled) return;
        if (prefabPeluru == null || mainCamera == null) return;

        Vector2 posisiMouseLayar = Mouse.current.position.ReadValue();
        Vector3 posisiMouseDunia = mainCamera.ScreenToWorldPoint(new Vector3(posisiMouseLayar.x, posisiMouseLayar.y, transform.position.z));
        Transform tempatMuncul = titikTembak != null ? titikTembak : transform;

        Vector2 arahTembak = ((Vector2)posisiMouseDunia - (Vector2)tempatMuncul.position).normalized;
        float sudutRotasi = Mathf.Atan2(arahTembak.y, arahTembak.x) * Mathf.Rad2Deg;

        GameObject peluruBaru = Instantiate(prefabPeluru, tempatMuncul.position, Quaternion.Euler(0, 0, sudutRotasi));

        Rigidbody2D rbPeluru = peluruBaru.GetComponent<Rigidbody2D>();
        if (rbPeluru != null)
        {
            rbPeluru.linearVelocity = arahTembak * kecepatanPeluru;
        }
    }
}