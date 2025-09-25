using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform player;            // Player reference
    public float detectionRange = 5f;   // Max distance to see player
    public GameObject projectilePrefab; // Bullet prefab
    public Transform firePoint;         // Bullet spawn point
    public float fireRate = 1f;         // Shots per second

    private float nextFireTime = 0f;
    private bool facingRight = true;    // Current facing direction

    void Update()
    {
        if (player != null)
        {
            Vector2 directionToPlayer = player.position - transform.position;

            // Flip enemy to face player
            if ((directionToPlayer.x < 0 && !facingRight) || (directionToPlayer.x > 0 && facingRight))
            {
                Flip();
            }

            // Check if player is in front (same direction as facing)
            bool playerInFront = (facingRight && directionToPlayer.x < 0) || (!facingRight && directionToPlayer.x > 0);

            // Check horizontal distance
            float distanceX = Mathf.Abs(directionToPlayer.x);

            if (playerInFront && distanceX <= detectionRange && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().moveRight = facingRight;
    }
}
