using UnityEngine;

public class GameplayMusic : MonoBehaviour
{
    void Awake()
    {
        GameObject musikMenu = GameObject.Find("BG_Homescreen");
        if (musikMenu != null)
        {
            Destroy(musikMenu);
        }
    }

    // FUNGSI BARU
    void Start()
    {
        AudioSource bgmGameplay = GetComponent<AudioSource>();
        if (bgmGameplay != null)
        {
            // Terapkan memori "VolumeGlobal" ke musik gameplay ini
            bgmGameplay.volume = PlayerPrefs.GetFloat("VolumeGlobal", 1f);
        }
    }
}