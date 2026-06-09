using UnityEngine;
using TMPro; // Penting untuk mengontrol TextMeshPro

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance; // Membuat skrip ini bisa diakses dengan mudah oleh skrip lain

    [Header("Sistem Poin")]
    public int currentPoints = 0;
    public TextMeshProUGUI pointText; // Tempat menaruh komponen teks UI nanti

    private void Awake()
    {
        // Setup Singleton pattern
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdatePointUI();
    }

    // Fungsi yang akan dipanggil saat monster mati
    public void AddPoints(int amount)
    {
        currentPoints += amount;
        UpdatePointUI();
    }

    public bool SpendPoints(int amount)
    {
    if (currentPoints >= amount)
    {
        currentPoints -= amount;
        UpdatePointUI();
        return true; // Poin cukup
    }
    Debug.Log("Poin tidak cukup!");
    return false; // Poin kurang
    }

    // Fungsi untuk memperbarui teks di layar
    private void UpdatePointUI()
    {
        if (pointText != null)
        {
            pointText.text = "Poin: " + currentPoints;
        }
    }
}