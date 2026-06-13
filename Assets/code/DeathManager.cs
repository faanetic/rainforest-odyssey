using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathManager : MonoBehaviour
{
    [Header("UI Dark Souls")]
    public CanvasGroup panelMatiCanvasGroup; // Isi dengan Canvas Group dari UI tulisan mati
    public float durasiFadeIn = 2f;          // Kecepatan tulisan muncul
    public float durasiTungguMati = 3f;      // Berapa lama layar mati bertahan sebelum pindah scene
    public string namaSceneTujuan = "Scene_Home"; // Nama scene input field/home screen kamu

    void Start()
    {
        // Pastikan UI mati tidak kelihatan di awal game
        if (panelMatiCanvasGroup != null)
        {
            panelMatiCanvasGroup.alpha = 0f;
            panelMatiCanvasGroup.gameObject.SetActive(false);
        }
    }

    public void ProsesKematianPlayer()
    {
        StartCoroutine(AlurKematianDarkSouls());
    }

    private IEnumerator AlurKematianDarkSouls()
    {
        if (panelMatiCanvasGroup == null)
        {
            // Jika lupa pasang UI, langsung pindah scene saja biar tidak error
            SceneManager.LoadScene(namaSceneTujuan);
            yield break;
        }

        panelMatiCanvasGroup.gameObject.SetActive(true);
        float timer = 0f;

        // 1. Efek Fade In Tulisan Mati
        while (timer < durasiFadeIn)
        {
            timer += Time.deltaTime;
            panelMatiCanvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / durasiFadeIn);
            yield return null;
        }
        panelMatiCanvasGroup.alpha = 1f;

        // 2. Tunggu beberapa saat (Masa meratapi kekalahan)
        yield return new WaitForSeconds(durasiTungguMati);

        // 3. Pindah ke Scene Home Screen / Input Nama secara langsung
        SceneManager.LoadScene(namaSceneTujuan);
    }
}