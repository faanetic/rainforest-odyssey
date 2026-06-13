using UnityEngine;
using System.Collections;

public class DroppedItemAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float kecepatanMelayang = 1f;
    public float durasiTampil = 1.5f;

    // --- KODE BARU: Mengatur ukuran item ---
    [Header("Pengaturan Ukuran")]
    [Tooltip("Ubah angka ini untuk memperbesar/memperkecil item. Misal: 2 atau 3")]
    public float ukuranSkala = 2.5f; 

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // --- KODE BARU: Paksa skala objek berubah sesuai ukuranSkala ---
        transform.localScale = new Vector3(ukuranSkala, ukuranSkala, 1f);
    }

    public void MulaiAnimasi(Sprite gambarItem)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = gambarItem;
        }
        StartCoroutine(EfekMelayangDanHilang());
    }

    private IEnumerator EfekMelayangDanHilang()
    {
        float waktuBerjalan = 0f;
        Color warnaAsli = spriteRenderer.color;

        while (waktuBerjalan < durasiTampil)
        {
            transform.Translate(Vector3.up * kecepatanMelayang * Time.deltaTime);

            float alpha = Mathf.Lerp(1f, 0f, waktuBerjalan / durasiTampil);
            spriteRenderer.color = new Color(warnaAsli.r, warnaAsli.g, warnaAsli.b, alpha);

            waktuBerjalan += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}