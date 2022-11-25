using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class Teleport : MonoBehaviour
{

    private Transform destination;

    public bool level1;
    public bool level2;
    public bool level3;

    public float distance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        if (level1 == true)
        {
            destination = GameObject.FindGameObjectWithTag("level2start").GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (Vector2.Distance(transform.position,other.transform.position) > distance)
            {
                other.transform.position = new Vector2 (destination.position.x, destination.position.y);
            }

        }
    }
}
