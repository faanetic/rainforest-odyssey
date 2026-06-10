using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            // BARIS BARU: Ambil komponen suara dan sesuaikan volumenya dari memori PlayerPrefs
            AudioSource audioSrc = GetComponent<AudioSource>();
            if (audioSrc != null)
            {
                // Jika belum ada memori (game baru diinstal), defaultnya adalah 1f (suara penuh)
                audioSrc.volume = PlayerPrefs.GetFloat("VolumeGlobal", 1f);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}