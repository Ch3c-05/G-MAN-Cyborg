using System.Collections;
using System.Collections.Generic;  
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private PlayerMovement playerMovement;

    void Start()
    {
        // Assumes WeaponScript is attached to the player or a child of the player
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Use player's facing direction
        Vector2 shootDirection = playerMovement != null && playerMovement.IsFacingRight ? Vector2.right : Vector2.left;

        bullet bulletScript = bulletObj.GetComponent<bullet>();
        bulletScript.SetDirection(shootDirection);
    }
}
