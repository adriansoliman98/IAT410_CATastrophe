using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    private readonly float fireForce = 15f;
    private readonly float catGunFireForce = 15f;
    private readonly float fireRate = 0.21f;
    private float nextFire = 0f;

    Vector2 moveDirection;
    Vector2 mousePosition;
    public Rigidbody2D rb;

    public void Fire()
    {

        if (gameObject.tag == "Catgun1")
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);

            Vector2 aimDirection = mousePosition - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
        }

        if (gameObject.tag == "Catgun")
        {

            if (Input.GetButtonDown("Fire1"))
           {
                nextFire = Time.time + fireRate;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * catGunFireForce, ForceMode2D.Impulse);

                Vector2 aimDirection = mousePosition - rb.position;
                float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = aimAngle;

           }

        }
    }



   
}
