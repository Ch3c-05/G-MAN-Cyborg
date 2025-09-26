using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public bool moveRight = true;

    void Start()
    {
        Destroy(gameObject, 3f);

        // Always move in the direction the enemy is facing
        float xVelocity = moveRight ? speed : -speed;
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(xVelocity, 0f);
    }

    void OnTriggerEnter2D()
    {
        Debug.Log("Enemy hit object");
        Destroy(gameObject);
    }
}
