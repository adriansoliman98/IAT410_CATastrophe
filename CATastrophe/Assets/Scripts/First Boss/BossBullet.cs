using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public Vector2 moveDirection;
    public float moveSpeed;
    public float bulletDamage;

    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }
    // Start is called before the first frame update
    void Start()
    {
       // moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void OnDestroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
          //  enemyComponent.TakeDamage(bulletDamage);
            Destroy(gameObject);

        }

        if (collision.gameObject.TryGetComponent<Furniture>(out Furniture furnitureComponent))
        {
           // furnitureComponent.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }




        Destroy(gameObject);
    }
}
