using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private AudioSource bgmAudio; 
    public Slider volumeSlider; 

    void Start()
    {
        GameObject objekMusik = GameObject.Find("BG_Homescreen");
        if (objekMusik != null)
        {
            bgmAudio = objekMusik.GetComponent<AudioSource>();
        }

        if (bgmAudio != null && volumeSlider != null)
        {
            volumeSlider.value = Mathf.Sqrt(bgmAudio.volume) * 50f;
        }
    }

    public void AturVolume(float nilaiVolume)
    {
        if (bgmAudio != null) 
        {
            float volumeNormal = nilaiVolume / 50f; 
            bgmAudio.volume = volumeNormal * volumeNormal; 
            
            // BARIS BARU: Simpan volume ini secara permanen dengan nama "VolumeGlobal"
            PlayerPrefs.SetFloat("VolumeGlobal", bgmAudio.volume);
            PlayerPrefs.Save();
        }
    }
}