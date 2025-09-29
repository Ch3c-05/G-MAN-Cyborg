using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform player;            // Player reference
    public float detectionRange = 5f;   // Max distance to see player
    public GameObject projectilePrefab; // Bullet prefab
    public Transform firePoint;         // Bullet spawn point
    public float fireRate = 1f;         // Shots per second
    public int health = 100;          // Enemy health
    public GameObject deathEffect;     

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

            bool playerInFront = (facingRight && directionToPlayer.x < 0) || (!facingRight && directionToPlayer.x > 0);
            float distanceX = Mathf.Abs(directionToPlayer.x);

            // Debug output
            Debug.Log($"FacingRight: {facingRight}, directionToPlayer.x: {directionToPlayer.x}, playerInFront: {playerInFront}, distanceX: {distanceX}");

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
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().moveRight = !facingRight;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }



}
