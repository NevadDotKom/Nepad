using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Kecepatan gerak
    public float jumpForce = 10f; // Kekuatan lompat
    public Rigidbody2D rb; // Komponen Rigidbody2D
    public LayerMask groundLayer; // Layer tanah untuk mendeteksi lompat
    public Transform groundCheck; // Posisi untuk mengecek tanah
    public Animator animator; // Komponen Animator
    public GameObject waterProjectilePrefab; // Prefab air
    public Transform shootPoint; // Titik sembur air

    private Vector2 movement;
    private bool isFacingRight = false; // Default menghadap kiri
    private bool isGrounded;
    private bool isRunning;
    private bool isJumping;

    void Update()
    {
        // Input horizontal untuk bergerak
        movement.x = Input.GetAxisRaw("Horizontal");

        // Cek apakah karakter menyentuh tanah
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Lompat jika tombol jump ditekan dan karakter di tanah
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJumping = true; // Set isJumping ke true saat lompat
        }

        // Jika karakter sudah menyentuh tanah, set isJumping ke false
        if (isGrounded && isJumping)
        {
            isJumping = false;
        }

        // Membalikkan karakter jika arah berubah
        if (movement.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && isFacingRight)
        {
            Flip();
        }

        // Mengatur animasi isRunning berdasarkan gerakan
        isRunning = movement.x != 0; // Jika karakter bergerak, animasi berjalan
        animator.SetBool("IsRunning", isRunning); // Set parameter IsRunning di Animator

        // Mengatur animasi isJumping
        animator.SetBool("IsJumping", isJumping); // Set parameter IsJumping di Animator

        // Menyembur air
        if (Input.GetButtonDown("Fire1")) // Misalnya "Fire1" adalah tombol untuk menyembur air
        {
            ShootWater();
        }
    }

    void FixedUpdate()
    {
        // Gerakkan karakter secara horizontal
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }

    void Flip()
    {
        // Membalikkan arah karakter
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void ShootWater()
    {
        // Menembakkan air
        GameObject waterProjectile = Instantiate(waterProjectilePrefab, shootPoint.position, Quaternion.identity);

        // Menambahkan kecepatan semburan
        Rigidbody2D waterRb = waterProjectile.GetComponent<Rigidbody2D>();
        if (isFacingRight)
        {
            waterRb.velocity = new Vector2(10f, 0f); // Ke kanan
        }
        else
        {
            waterRb.velocity = new Vector2(-10f, 0f); // Ke kiri
        }
        
    }
}
