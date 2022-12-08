using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{

    public GameObject plate;
    public GameObject plate2;
    public GameObject plate3;
    public GameObject plate4;   
  
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
            plate2.transform.position = new Vector2(plate2.transform.position.x, plate2.transform.position.y - 10);
            plate3.transform.position = new Vector2(plate3.transform.position.x, plate3.transform.position.y - 10);
            Destroy(gameObject);
              
            
        }

        if (this.gameObject.tag == "WaterPlate" && other.CompareTag("Player"))
        {

            plate4.transform.position = new Vector2(plate4.transform.position.x, plate4.transform.position.y - 10);
            plate2.transform.position = new Vector2(plate2.transform.position.x, plate2.transform.position.y - 10);
            plate3.transform.position = new Vector2(plate3.transform.position.x, plate3.transform.position.y - 10);
            Destroy(gameObject);


        }

        if (this.gameObject.tag == "EarthPlate" && other.CompareTag("Player"))
        {

            plate.transform.position = new Vector2(plate.transform.position.x, plate.transform.position.y - 10);
            
            plate3.transform.position = new Vector2(plate3.transform.position.x, plate3.transform.position.y - 10);
            plate4.transform.position = new Vector2(plate4.transform.position.x, plate4.transform.position.y - 10);
            Destroy(gameObject);


        }

        if (this.gameObject.tag == "FirePlate" && other.CompareTag("Player"))
        {

            plate.transform.position = new Vector2(plate.transform.position.x, plate.transform.position.y - 10);
            plate2.transform.position = new Vector2(plate2.transform.position.x, plate2.transform.position.y - 10);
            plate4.transform.position = new Vector2(plate4.transform.position.x, plate4.transform.position.y - 10);
            Destroy(gameObject);


        }
    }
}
