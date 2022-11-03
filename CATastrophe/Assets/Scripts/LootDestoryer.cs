using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LootDestoryer : MonoBehaviour
{
    LootBag lootitem;
    public GameObject lootobject;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
      //  lootitem = lootitem.GetComponent<LootBag>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //    if (gameObject.tag == "Player")

        /*
           if (gameObject.tag == "bow")
           {
               if (collision.gameObject.name == "Player")
               {

                  playerController = playerController.GetComponent<PlayerController>();
                   playerController.bow = true;
                   Destroy(this.gameObject);

                   Destroy(gameObject);


               }
           }
           lootitem = lootitem.GetComponent<Loot>();
           */
        //   if (gameObject.tag == "Item") { 
        //lootitem = lootitem.GetComponent<LootBag>();
     //   if (lootitem.lootName == "bow") { 

        if (collision.gameObject.tag == "Player")
        {
                          

            Destroy(this.gameObject);

            Destroy(gameObject);

        }
    }
}
