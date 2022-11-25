using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWorldSpawner : MonoBehaviour
{

    public WeaponItem weaponItem;
    public GameObject droppedItemPrefab;
    // Start is called before the first frame update
    private void Awake()
    {
       // WeaponWorld.SpawnWeaponWorld(transform.position, weaponItem);
       // print(transform.position.x);  
        //Destroy(gameObject);
    }

    private void DropLoot()
    {
        WeaponWorld.SpawnWeaponWorld(transform.position, weaponItem);
        Destroy(gameObject);
        print(transform.position.x);
    }

    public void InstantiateLoot()
    {

      
            WeaponWorld.SpawnWeaponWorld(transform.position, weaponItem);
           

          
        }


    

    // Update is called once per frame
    void Update()
    {

       // print(transform.position.x);
       // print(transform.position.y);
    }

}
