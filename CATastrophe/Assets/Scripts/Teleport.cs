using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class Teleport : MonoBehaviour
{

    private Transform destination;

   

    public float distance = 5f;

    // Start is called before the first frame update
    void Start()
    {
       // if (level1 == true)
      //  {
       //     destination = GameObject.FindGameObjectWithTag("level2start").GetComponent<Transform>();
       // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void OnTriggerEnter2D(Collider2D other)
    {
        if (this.gameObject.tag == "Level1Exit" && other.CompareTag("Player"))
        {
            destination = GameObject.FindGameObjectWithTag("level2start").GetComponent<Transform>();
            if (Vector2.Distance(transform.position,other.transform.position) > distance)
            {
                other.transform.position = new Vector2 (destination.position.x, destination.position.y);
            }

        }

        if (this.gameObject.tag == "Level2Exit" && other.CompareTag("Player"))
        {
            destination = GameObject.FindGameObjectWithTag("Level3Start").GetComponent<Transform>();
            if (Vector2.Distance(transform.position, other.transform.position) > distance)
            {
                other.transform.position = new Vector2(destination.position.x, destination.position.y);
            }

        }
    }
}
