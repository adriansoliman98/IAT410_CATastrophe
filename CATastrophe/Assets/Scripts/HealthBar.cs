using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{   
   public  SpriteRenderer spriteRenderer;
     public PlayerController playerController;
    public GameObject player;
    public Sprite health1, health2, health3;
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void Update()
    {
        playerController = player.GetComponent<PlayerController>();
        print("health 3");
        print(playerController.playerHealth);
        if (playerController.playerHealth == 3)
        {
            GetComponent<SpriteRenderer>().sprite = health3;
            spriteRenderer.sprite = health3;
            print("health 3");
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            GetComponent<SpriteRenderer>().sprite = health2;
            spriteRenderer.sprite = health2;
        }

    
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("health 3");
            spriteRenderer.sprite = health1;
            GetComponent<SpriteRenderer>().sprite = health1;
        }
    }
}
