using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneController : MonoBehaviour
{
    // Fungsi untuk pindah ke scene Setting secara langsung
    public void PindahKeSetting(string namaSceneSetting)
    {
        SceneManager.LoadScene(namaSceneSetting);
    }

    // Fungsi untuk kembali ke Main Menu dari Setting
    public void KembaliKeMainMenu(string namaSceneMainMenu)
    {
        SceneManager.LoadScene(namaSceneMainMenu);
    }
}
