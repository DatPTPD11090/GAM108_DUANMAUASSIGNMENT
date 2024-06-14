using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;
    float moveSpeed = 5f;
    float rollSpeed = 10f; // Tốc độ di chuyển khi lăn lộn
    float climbSpeed = 5f; // Tốc độ di chuyển khi leo thang
    bool isFacingRight = true; // Bắt đầu với việc hướng phải
    float jumpPower = 6f;
    bool isOnGround = false;
    bool isClimbing = false; // Biến kiểm tra nếu nhân vật đang leo thang

    public GameObject arrowPrefab; 
    public Transform arrowSpawnPoint; 
    float shootCooldown = 1f;
    float lastShootTime;

    Rigidbody2D rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player"); 

        if (player != null)
        {
            rb = player.GetComponent<Rigidbody2D>();
            animator = player.GetComponent<Animator>();
        }

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing on the player GameObject.");
        }
        if (animator == null)
        {
            Debug.LogError("Animator component is missing on the player GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (isClimbing)
        {
            verticalInput = Input.GetAxis("Vertical");
        }

        FlipSprite();

        if (Input.GetButtonDown("Jump") && isOnGround && !isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isOnGround = false;
            animator.SetBool("isJumping", !isOnGround);
        }

        if (Input.GetKeyDown(KeyCode.F) && !isClimbing)
        {
            animator.SetBool("isRolling", true);
            moveSpeed = rollSpeed; // Thay đổi tốc độ di chuyển khi lăn lộn
        }
        else if (Input.GetKeyUp(KeyCode.F) && !isClimbing)
        {
            animator.SetBool("isRolling", false);
            moveSpeed = 5f; // Trả lại tốc độ di chuyển ban đầu
        }

        
        if (Input.GetKeyDown(KeyCode.E) && Time.time > lastShootTime + shootCooldown)
        {
            animator.SetBool("isShooting", true);
            Shoot();
            lastShootTime = Time.time;
        }
        
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);
            rb.gravityScale = 0; // Tắt trọng lực khi leo thang
            animator.SetFloat("yVelocity", Mathf.Abs(rb.velocity.y));

            // Vô hiệu hóa chức năng lăn lộn khi đang leo thang
            if (Input.GetKeyDown(KeyCode.F))
            {
                animator.SetBool("isRolling", false);
            }
        }
        else
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            rb.gravityScale = 1; // Bật trọng lực khi không leo thang
            animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
            animator.SetFloat("yVelocity", rb.velocity.y);
        }
    }

    void FlipSprite()
    {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f; // Đổi dấu để quay nhân vật
            transform.localScale = ls;
        }
    }

    void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rbArrow = arrow.GetComponent<Rigidbody2D>();


        float arrowDirection = isFacingRight ? 1f : -1f;
        rbArrow.velocity = new Vector2(arrowDirection * 10f, 0);

        Destroy(arrow, 1.5f);

        StartCoroutine(ResetShootingAnimation());
    
    }

    IEnumerator ResetShootingAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("isShooting", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isOnGround = true;
            animator.SetBool("isJumping", !isOnGround);

            // Stop climbing if the player touches the ground
            if (isClimbing)
            {
                isClimbing = false;
                rb.gravityScale = 1; // Re-enable gravity
                animator.SetBool("IsClimbing", false);
            }
        }
        else if (collision.CompareTag("Ladder"))
        {
            isClimbing = true;
            
            rb.gravityScale = 0; // Disable gravity when climbing
            animator.SetBool("IsClimbing", true);
        }
    }

        private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = false;
            animator.SetBool("IsClimbing", false);
        }
        if (collision.gameObject.tag.Equals("Enemy"))
        {

            Vector2 fistPosition = new Vector2(-8.56f, -2.01f);
            transform.position = fistPosition;

        }
        if (collision.gameObject.tag.Equals("spike"))
        {

            Vector2 fistPosition = new Vector2(-8.56f, -2.01f);
            transform.position = fistPosition;

        }
    }
}




