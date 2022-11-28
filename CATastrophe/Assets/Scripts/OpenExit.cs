using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenExit : MonoBehaviour

{
    //public GameObject gameObject;
    
    public Enemy enemy;
    // Start is called before the first frame update


    private void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
    }


    // Update is called once per frame
    void Update()
    {
        enemy = gameObject.GetComponent<Enemy>();

        if (gameObject.tag == "firstBoss")
            if (enemy.bossDead == true)
        {
            transform.position = new Vector2(transform.position.x + 1, transform.position.y );
        }

        if (gameObject.tag == "secondBoss1" && gameObject.tag == "secondBoss2")
        {
            if (enemy.bossDead == true)
            {
                transform.position = new Vector2(transform.position.x + 1, transform.position.y);
            }

           
        }

        if (gameObject.tag == "thirdBoss")
            if (enemy.bossDead == true)
            {
                transform.position = new Vector2(transform.position.x + 1, transform.position.y);
            }

        //  print(enemy.bossDead);
    }
}
