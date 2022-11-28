using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{

    public GameObject plate;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (this.gameObject.tag == "Level3BossPlate" && other.CompareTag("Player"))
        {
           
                plate.transform.position = new Vector2(plate.transform.position.x , plate.transform.position.y -10 );
            Destroy(gameObject);
              
            
        }
    }
}
