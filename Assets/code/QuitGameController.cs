using UnityEngine;

public class QuitGameController : MonoBehaviour
{
    [Header("UI Pop-Up Konfirmasi")]
    [SerializeField] private GameObject panelKonfirmasiQuit;

    private void Start()
    {
        // Pastikan pop-up dalam keadaan mati saat game dimulai
        if (panelKonfirmasiQuit != null)
        {
            panelKonfirmasiQuit.SetActive(false);
        }
    }

    // Fungsi yang dipanggil saat tombol Keluar/Quit di Main Menu diklik
    public void TampilkanPopUpKonfirmasi()
    {
        if (panelKonfirmasiQuit != null)
        {
            panelKonfirmasiQuit.SetActive(true);
        }
    }

    // Fungsi jika player memilih tombol "Ya" (Konfirmasi Keluar)
    public void QuitGame()
    {
        Debug.Log("Game Keluar (Fungsi ini berjalan saat game sudah di-build)");
        Application.Quit();
    }

    // Fungsi jika player memilih tombol "Tidak" (Membatalkan Keluar)
    public void BatalkanQuit()
    {
        if (panelKonfirmasiQuit != null)
        {
            panelKonfirmasiQuit.SetActive(false);
        }
    }
}
