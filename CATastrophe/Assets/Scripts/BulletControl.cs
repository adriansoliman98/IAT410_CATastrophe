using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SteralizedField] float speed;
    //[SteralizedField] float damage;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;
    public float lifeTime;
    public float bulletDamage;
    public float bulletHealth;
    [SerializeField] private UI_Inventory uiInventory;
    


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

    public void StopFire()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (gameObject.tag != "Flame" )
        {
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
            {

                  enemyComponent.TakeDamage(bulletDamage);
                Destroy(gameObject);

            }

            if (collision.gameObject.TryGetComponent<MouseAI>(out MouseAI mouseAIComponent))
            {
                mouseAIComponent.TakeDamage(bulletDamage);
                Destroy(gameObject);

            }

            if (collision.gameObject.TryGetComponent<Furniture>(out Furniture furnitureComponent))
            {
                furnitureComponent.TakeDamage(bulletDamage);
                Destroy(gameObject);
            }
            if (collision.gameObject.tag == "room")
            {
                Destroy(gameObject);
            }

        }

        
        
        if (gameObject.tag == "Catgun")
        {

            if (collision.gameObject.tag == "BossBullet")
            {
                // furnitureComponent.TakeDamage(bulletDamage);
                Destroy(gameObject);
            }
        }

        /*  if (gameObject.tag == "Stick")
          {
              if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
              {
                  Destroy(gameObject);
              }

              if (collision.gameObject.TryGetComponent<BossBullet>(out BossBullet bossbulletComponent))
              {

                  Destroy(gameObject);
              }

          }
        */
        //
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (gameObject.tag == "Flame" || gameObject.tag != "Air")
        {
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            

                enemyComponent.TakeDamage(bulletDamage);
            }

        }

       // if (gameObject.tag == "Stick")
     //   {
       //     if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
       //     {
       //         Destroy(gameObject);
       //     }


      //  }

        

        if (collision.gameObject.tag == "room")
        {
            Destroy(gameObject);
        }

       
    }

    /*  public void OnTriggerEnter2D(Collision2D collision)
      {
          if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
          {
              if (gameObject.tag == "Flame")
              {

                  enemyComponent.TakeDamage(bulletDamage);
              }

          }
      }
    */
}
