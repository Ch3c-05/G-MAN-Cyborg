using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 20f;
    private bool isFacingRight = true;

    public bool IsFacingRight => isFacingRight;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Platform Drop Settings")]
    public float dropTime = 5f; // how long collision is disabled

    private PlatformEffector2D currentEffector;
    private float dropTimer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        // Variable jump height
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        // Drop through platform (press "S" or DownArrow)
        if (currentEffector != null && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            dropTimer = dropTime;
            currentEffector.rotationalOffset = 180f; // flips effector so we fall through
        }

        // Reset effector after drop
        if (dropTimer > 0)
        {
            dropTimer -= Time.deltaTime;
            if (dropTimer <= 0 && currentEffector != null)
            {
                currentEffector.rotationalOffset = 0f; // reset back to normal
            }
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out PlatformEffector2D effector))
        {
            currentEffector = effector;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out PlatformEffector2D effector) && effector == currentEffector)
        {
            currentEffector = null;
        }
    }
}
