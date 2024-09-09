using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Jika enemy sedang terkena knockback, maka abaikan pergerakan
        if (knockback.GettingKnockedBack) { return; }

        // Memindahkan posisi Rigidbody berdasarkan arah pergerakan dan kecepatan
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        // Menentukan arah flip sprite berdasarkan arah pergerakan
        if (moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveDir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    // Fungsi untuk mengatur arah pergerakan menuju posisi target
    public void MoveTo(Vector2 targetPosition)
    {
        // Menghitung arah dari posisi saat ini menuju posisi target
        moveDir = (targetPosition - rb.position).normalized;
    }

    // Fungsi untuk menghentikan pergerakan enemy
    public void StopMoving()
    {
        moveDir = Vector2.zero;
    }
}
