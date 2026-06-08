using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem; 

public class movement : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody2D rb;
    public Animator anim;
    
    // Pastikan ini PUBLIC agar bisa dibaca oleh script PlayerAttack
    public int facingDirection = 1;

    [Header("Batas Gerak Player (Map Clamp)")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
   
    void FixedUpdate()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) 
                moveHorizontal = 1f;
            else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) 
                moveHorizontal = -1f;

            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) 
                moveVertical = 1f;
            else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) 
                moveVertical = -1f;
        }

        if(moveHorizontal > 0 && transform.localScale.x < 0 ||
           moveHorizontal < 0 && transform.localScale.x > 0)
        {
            flip();
        }

        anim.SetFloat("moveVertical", Mathf.Abs(moveVertical));
        anim.SetFloat("moveHorizontal", Mathf.Abs(moveHorizontal));

        // Hitung kecepatan awal berdasarkan input
        Vector2 targetVelocity = new Vector2(moveHorizontal, moveVertical) * speed;

        // Solusi anti-jitter batas gerak map
        if (rb.position.x <= minX && targetVelocity.x < 0) targetVelocity.x = 0;
        if (rb.position.x >= maxX && targetVelocity.x > 0) targetVelocity.x = 0;
        if (rb.position.y <= minY && targetVelocity.y < 0) targetVelocity.y = 0;
        if (rb.position.y >= maxY && targetVelocity.y > 0) targetVelocity.y = 0;

        rb.linearVelocity = targetVelocity;
    }

    void flip()
    {
        facingDirection *= -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnDisable()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            if (anim != null)
            {
                anim.SetFloat("moveHorizontal", 0);
                anim.SetFloat("moveVertical", 0);
            }
        }
    }
}