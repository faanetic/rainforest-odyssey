using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Tarik GameObject Player kamu ke slot ini di Inspector
    public Transform targetPlayer; 

    // Kecepatan kamera mengikuti player (makin kecil makin halus efek smooth-nya)
    public float smoothSpeed = 0.125f; 

    // BATAS MAP (Isi nilai koordinat ini di Inspector setelah mengukur ujung map)
    [Header("Batas Kamera (Camera Clamp)")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void FixedUpdate()
{
    if (targetPlayer == null) return;

    // ... sisa kode Clamp dan Lerp kamu yang kemarin tetap sama ...
    Vector3 posisiTujuan = new Vector3(targetPlayer.position.x, targetPlayer.position.y, transform.position.z);
    float posisiXTerkuci = Mathf.Clamp(posisiTujuan.x, minX, maxX);
    float posisiYTerkuci = Mathf.Clamp(posisiTujuan.y, minY, maxY);
    Vector3 posisiTerkuci = new Vector3(posisiXTerkuci, posisiYTerkuci, transform.position.z);

    // Lerp di sini sekarang berjalan selaras dengan pergerakan player
    transform.position = Vector3.Lerp(transform.position, posisiTerkuci, smoothSpeed);
}
}