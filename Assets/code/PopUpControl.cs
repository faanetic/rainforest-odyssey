using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PopUpManager : MonoBehaviour
{
    [Header("Pengaturan Jeda (Global Keyboard)")]
    [SerializeField] private GameObject panelPauseUtama; 
    private bool isPaused = false;

    void Update()
    {
        // Fitur global untuk tombol Escape (Menutup/Membuka jeda game)
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (panelPauseUtama != null)
            {
                if (isPaused)
                    TutupPopUpDanResume(panelPauseUtama);
                else
                    BukaPopUpDanPause(panelPauseUtama);
            }
        }
    }

    // ========================================================
    // FUNGSI GENERIC (BISA UNTUK POP-UP APA SAJA)
    // ========================================================

    // Fungsi dasar hanya untuk menampilkan Pop-Up (Misal: Menu Setting, Achievement, Info, dll)
    public void BukaPopUp(GameObject panelTarget)
    {
        if (panelTarget != null) 
        {
            panelTarget.SetActive(true);
        }
    }

    // Fungsi dasar hanya untuk menyembunyikan Pop-Up
    public void TutupPopUp(GameObject panelTarget)
    {
        if (panelTarget != null) 
        {
            panelTarget.SetActive(false);
        }
    }

    // Fungsi khusus jika pop-up tersebut sekaligus menghentikan waktu game (Pause)
    public void BukaPopUpDanPause(GameObject panelTarget)
    {
        if (panelTarget != null)
        {
            panelTarget.SetActive(true);
            Time.timeScale = 0f; // Hentikan waktu dunia game
            if (panelTarget == panelPauseUtama) isPaused = true;
        }
    }

    // Fungsi khusus jika pop-up tersebut mengembalikan waktu game (Resume)
    public void TutupPopUpDanResume(GameObject panelTarget)
    {
        if (panelTarget != null)
        {
            panelTarget.SetActive(false);
            Time.timeScale = 1f; // Kembalikan waktu normal
            if (panelTarget == panelPauseUtama) isPaused = false;
        }
    }

    // ========================================================
    // FUNGSI PINDAH SCENE (ANTI FREEZE)
    // ========================================================
    
    // KUNCI PERBAIKAN: Fungsi khusus untuk tombol "Home" di dalam Panel Pause
    public void KembaliKeHome(string namaSceneHome)
    {
        Time.timeScale = 1f; // PENTING: Kembalikan waktu ke normal dulu agar scene home tidak membeku!
        isPaused = false;
        SceneManager.LoadScene(namaSceneHome);
    }

    public void RestartSceneAktif()
    {
        Time.timeScale = 1f; // Pastikan waktu normal sebelum restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}