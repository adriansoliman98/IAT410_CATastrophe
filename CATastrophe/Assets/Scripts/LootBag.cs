using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LootBag : MonoBehaviour
{

     public Sprite lootSprite;
    public string lootName;
    public int dropChance;
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();

    private void Start()
    {
    //    lootList.Add(new Loot("Bow", 80));
    }
    Loot GetDroppedItem()

    {
        int randomNumber = Random.Range(1, 101);
        List<Loot> possibleItems = new List<Loot>();
        foreach (Loot item in lootList)
        {
            if(randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
              
            }
        }
        if(possibleItems.Count > 0)
        {
            Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }

        Debug.Log("No loot dropped");
        return null;
    
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {

        Loot droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;

            float dropForce = 300f;
            Vector2 dropDirecton = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirecton * dropForce, ForceMode2D.Impulse);
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //    if (gameObject.tag == "Player")
    
        if (collision.gameObject.name == "Arrow")
        {
          
            Destroy(this.gameObject);

            Destroy(gameObject);
            GameObject.Destroy(gameObject);
        }

        if (collision.gameObject.tag.Equals("Arrow"))
        {
           
            Destroy(this.gameObject);

            Destroy(gameObject);
            GameObject.Destroy(gameObject);
        }
    }





}
