using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public bool moveRight = true;
    public int damage = 20; // how much damage this bullet does

    void Start()
    {
        Destroy(gameObject, 3f);

        float xVelocity = moveRight ? speed : -speed;
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(xVelocity, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore other enemy bullets
        if (other.CompareTag("EnemyBullet"))
            return;

        // Hit the player
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject);
            return;
        }

        // Hit the ground
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
