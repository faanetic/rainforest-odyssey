using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "GachaSystem/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon; // Gambar yang akan tampil di Gallery

    // --- TAMBAHKAN KODE BARU DI BAWAH INI ---
    [TextArea(4, 15)]
    public string itemDescription; // Tempat menulis paragraf penjelasan item di papan cokelat
}