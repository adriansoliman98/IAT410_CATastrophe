using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 3f;

    public float moveSpeed1;
    public float checkRadius1;
   

    public LayerMask playerMask;

    private Transform target1;
    public Rigidbody2D rb2;

    private Vector2 movement;
    private Vector3 dir;
 

    private bool isInChaseRange;
    


    private void Start()
    {
        health = maxHealth;


        //   rb = GetComponent<Rigidbody2D>();
        target1 = GameObject.FindWithTag("Player").transform;

    }

    private void Update()
    {
     

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius1, playerMask);
       

        dir = target1.position - transform.position;
       
        dir.Normalize();
        movement = dir;
       

    }

    private void FixedUpdate()
    {

        if (isInChaseRange)
        {

            MoveCharacter();

        }

      else
        {

            rb2.velocity = Vector2.zero;
        }


    }

    private void MoveCharacter()
    {
        rb2.MovePosition((Vector2)transform.position + (movement * moveSpeed1 * Time.deltaTime));


    }

    public void PlayerHit()
    {
        rb2.MovePosition((Vector2)transform.position *-2  - (movement * moveSpeed1 * 2));


    }

    public void TakeDamage(float damageAmount)
    {
      /*  health -= damageAmount;

        GetComponent<LootBag>().InstantiateLoot(transform.position);

        if (health <= 0)
        {
            GetComponent<LootBag>().InstantiateLoot(transform.position);
            Die();
        }
      */
    }


    void Die()
    {
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        Destroy(gameObject);
    }

    // Update is called once per frame


}
