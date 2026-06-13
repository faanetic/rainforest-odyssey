using UnityEngine;
using UnityEngine.SceneManagement; // Wajib diimport untuk manajemen scene

public class ManajerScene : MonoBehaviour
{
    // Fungsi untuk pindah scene berdasarkan NAMA scene
    public void PindahSceneLewatNama(string namaScene)
    {
        SceneManager.LoadScene(namaScene);
    }

    // Fungsi untuk pindah scene berdasarkan INDEX di Build Settings
    public void PindahSceneLewatIndex(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
    }

    // Fungsi untuk mengulang scene yang sedang aktif saat ini (misal untuk restart)
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Fungsi untuk keluar dari game
    public void KeluarGame()
    {
        // Perintah untuk menutup aplikasi game yang sudah di-build
        Application.Quit();

        // Catatan: Application.Quit() tidak akan menghentikan mode "Play" di Unity Editor.
        // Baris di bawah ini berfungsi agar tombol keluar tetap bekerja saat kamu testing di Editor.
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        
        Debug.Log("Game Telah Keluar");
    }
}