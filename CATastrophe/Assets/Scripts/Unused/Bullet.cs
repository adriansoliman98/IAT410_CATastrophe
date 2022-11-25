using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
       /* if (gameObject.tag == "Catgun") { 

        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.TakeDamage((float)0.25);
        }
    }


        if (gameObject.tag == "Arrow")
        {

            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
            {
                print("hit by arrow");
                enemyComponent.TakeDamage((float)2);
            }
        }


        Destroy(gameObject);
       */
    }
}
