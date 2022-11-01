using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SteralizedField] float speed;
    [SteralizedField] float damage;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;
    public float lifeTime ;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Catgun")
        {

            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
            {
                enemyComponent.TakeDamage((float)1);
            }


          
        }

        if (gameObject.tag == "Arrow")
        {

            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
            {
                enemyComponent.TakeDamage((float)6);
            }
        }
        Destroy(gameObject);
    }

}
