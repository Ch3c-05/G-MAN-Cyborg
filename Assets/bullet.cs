using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 50;
    public Rigidbody2D rb;

    private Vector2 direction = Vector2.right;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = direction * speed;
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D hitinfo)
    {
        if(hitinfo.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
        {
            return; // Ignore collisions with the player
        }


        EnemyScript enemy = hitinfo.GetComponent<EnemyScript>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }


        if (hitinfo.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

    }

}
