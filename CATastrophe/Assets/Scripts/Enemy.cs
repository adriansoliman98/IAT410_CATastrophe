using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 3f;

    public float moveSpeed;
    public float checkRadius;
    public float attackRadius;

    public bool shouldRotate;

    public LayerMask playerMask;

    private Transform target;
    public Rigidbody2D rb;
    private Vector2 movement;
    public Vector3 dir;
    private Animator anim;

    private bool isInChaseRange;
    private bool isInAttackRange;


    private void Start()
    {
        health = maxHealth;

        target = GameObject.FindWithTag("Player").transform;

    }

    private void Update()
    {
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, playerMask);


        dir = target.position - transform.position;

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

            rb.velocity = Vector2.zero;
        }


    }

    private void MoveCharacter()
    {
        rb.MovePosition((Vector2)transform.position + (movement * moveSpeed * Time.deltaTime));


    }
    // Update is called once per frame
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;



        if (health <= 0)
        {
            Destroy(gameObject);
        }


    }
}
