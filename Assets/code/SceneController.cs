using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public RectTransform awanKiri;
    public RectTransform awanKanan;
    public float durasiTransisi = 0.5f;

    // Nilai posisi luar layar (atur sesuai ukuran awanmu agar sembunyi saat game mulai)
    // Misal: posisi X awal awan kiri adalah -1000, awan kanan adalah 1000
    private Vector2 posisiLuarKiri;
    private Vector2 posisiLuarKanan;

    private void Start()
    {
        // Catat posisi awal awan (posisi saat tersembunyi di luar layar)
        posisiLuarKiri = awanKiri.anchoredPosition;
        posisiLuarKanan = awanKanan.anchoredPosition;

        // KETIKA SCENE BARU TERBUKA: 
        // Paksa awan berada di tengah layar terlebih dahulu, lalu gerakkan membuka ke luar
        awanKiri.anchoredPosition = new Vector2(0, awanKiri.anchoredPosition.y);
        awanKanan.anchoredPosition = new Vector2(0, awanKanan.anchoredPosition.y);

        // Jalankan animasi membuka
        StartCoroutine(AlurAwanMembuka());
    }

    // --- FUNGSI UNTUK BUTTON (MENUTUP & PINDAH SCENE) ---
    public void PindahScene(string namaSceneBaru)
    {
        StartCoroutine(AlurTransisiScene(namaSceneBaru));
    }

    private IEnumerator AlurTransisiScene(string namaSceneBaru)
    {
        float timer = 0;
        Vector2 posisiTengahKiri = new Vector2(0, awanKiri.anchoredPosition.y);
        Vector2 posisiTengahKanan = new Vector2(0, awanKanan.anchoredPosition.y);

        // Awan bergerak menutup ke tengah
        while (timer < durasiTransisi)
        {
            timer += Time.deltaTime;
            float t = timer / durasiTransisi;
            awanKiri.anchoredPosition = Vector2.Lerp(posisiLuarKiri, posisiTengahKiri, t);
            awanKanan.anchoredPosition = Vector2.Lerp(posisiLuarKanan, posisiTengahKanan, t);
            yield return null;
        }

        SceneManager.LoadScene(namaSceneBaru);
    }

    // --- FUNGSI BARU: ANIMASI MEMBUKA SAAT SCENE BARU MULAI ---
    private IEnumerator AlurAwanMembuka()
    {
        float timer = 0;
        Vector2 posisiTengahKiri = new Vector2(0, awanKiri.anchoredPosition.y);
        Vector2 posisiTengahKanan = new Vector2(0, awanKanan.anchoredPosition.y);

        // Tunggu jeda sangat singkat agar scene baru benar-benar siap render
        yield return new WaitForSeconds(0.1f);

        // Awan bergerak membuka keluar layar
        while (timer < durasiTransisi)
        {
            timer += Time.deltaTime;
            float t = timer / durasiTransisi;
            awanKiri.anchoredPosition = Vector2.Lerp(posisiTengahKiri, posisiLuarKiri, t);
            awanKanan.anchoredPosition = Vector2.Lerp(posisiTengahKanan, posisiLuarKanan, t);
            yield return null;
        }
    }
}