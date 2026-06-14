using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public static CollectionManager Instance;

    [Header("Daftar Semua Master Item Data")]
    public List<ItemData> allMasterItems = new List<ItemData>();

    [Header("Gudang Koleksi Player Saat Ini")]
    public List<ItemData> myCollection = new List<ItemData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            
            LoadPermanentCollection();
        }
        else 
        { 
            Destroy(gameObject); 
        }
    }

    public void AddToCollection(ItemData newItem)
    {
        if (newItem == null) return;

        if (!myCollection.Contains(newItem))
        {
            myCollection.Add(newItem);
            Debug.Log("CollectionManager: Berhasil mendapat item baru -> " + newItem.itemName);

            PlayerPrefs.SetInt("Unlock_" + newItem.itemName, 1);
            PlayerPrefs.Save(); 
        }
    }

    private void LoadPermanentCollection()
    {
        myCollection.Clear();

        foreach (ItemData item in allMasterItems)
        {
            if (item == null) continue;

            // Membaca data kunci dari PlayerPrefs harddisk
            if (PlayerPrefs.GetInt("Unlock_" + item.itemName, 0) == 1)
            {
                myCollection.Add(item);
            }
        }
        Debug.Log("CollectionManager: Selesai memuat " + myCollection.Count + " item dari penyimpanan permanen.");
    }

    // FUNGSI SINKRONISASI: Dipanggil oleh GalleryUI saat scene terbuka
    public void MintaUpdateTampilanGaleri(GalleryUI uiGaleri)
    {
        LoadPermanentCollection(); // Pastikan data paling segar dari harddisk dimuat

        if (uiGaleri != null)
        {
            uiGaleri.EksekusiCetakSlot(myCollection); // Suapi data matang ke UI
        }
    }

    [ContextMenu("Hapus Semua Data Koleksi")]
    public void ResetSemuaKoleksiPermanen()
    {
        foreach (ItemData item in allMasterItems)
        {
            if (item != null) PlayerPrefs.DeleteKey("Unlock_" + item.itemName);
        }
        myCollection.Clear();
        PlayerPrefs.Save();
        Debug.Log("CollectionManager: Semua data achievement berhasil dihapus!");
    }
}