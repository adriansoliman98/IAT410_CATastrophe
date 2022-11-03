using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{

    [SerializeField] float health;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;



        if (health <= 0)
        {

            // GetComponent<ItemSpawner>().Spawn();
           // GetComponent<LootBag>().InstantiateLoot(transform.position);
            Destroy(gameObject);

        }


    }
}
