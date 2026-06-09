using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "GachaSystem/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon; // Gambar yang akan tampil di Gallery
}