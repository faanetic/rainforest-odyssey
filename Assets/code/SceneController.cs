using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    // Masukkan GameObject Awan Kiri dan Awan Kanan dari Inspector
    public RectTransform awanKiri;
    public RectTransform awanKanan;

    public float durasiTransisi = 0.5f;

    // Fungsi utama yang dipanggil saat player memicu perpindahan scene (misal: masuk portal)
    public void PindahScene(string namaSceneBaru)
    {
        StartCoroutine(AlurTransisiScene(namaSceneBaru));
    }

    private IEnumerator AlurTransisiScene(string namaSceneBaru)
    {
        // --- TAHAP 1: AWAN MENUTUP ---
        float timer = 0;
        // Posisi tengah layar untuk UI biasanya adalah 0 jika Anchor berada di center
        Vector2 posisiTengahKiri = new Vector2(0, awanKiri.anchoredPosition.y);
        Vector2 posisiTengahKanan = new Vector2(0, awanKanan.anchoredPosition.y);
        
        Vector2 posisiAwalKiri = awanKiri.anchoredPosition;
        Vector2 posisiAwalKanan = awanKanan.anchoredPosition;

        while (timer < durasiTransisi)
        {
            timer += Time.deltaTime;
            float t = timer / durasiTransisi;
            
            // Menggunakan Lerp untuk menggeser posisi awan secara halus
            awanKiri.anchoredPosition = Vector2.Lerp(posisiAwalKiri, posisiTengahKiri, t);
            awanKanan.anchoredPosition = Vector2.Lerp(posisiAwalKanan, posisiTengahKanan, t);
            yield return null;
        }

        // --- TAHAP 2: GANTI SCENE ---
        // Layar sekarang sudah tertutup awan sepenuhnya, aman untuk ganti scene
        SceneManager.LoadScene(namaSceneBaru);
    }
}