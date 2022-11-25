using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceHealthBar : MonoBehaviour
{
    public PlayerController playerController;
    public Sprite HB0, HB1, HB2, HB3;
    public float healthBar;
    public Transform toFollow;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        offset = toFollow.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerController = FindObjectOfType<PlayerController>();
        //print(playerController.playerHealth);
        if (playerController.playerHealth == 3)
        {
            //print(playerController.playerHealth);
            GetComponent<SpriteRenderer>().sprite = HB3;
        }
        if (playerController.playerHealth == 2)
        {
            GetComponent<SpriteRenderer>().sprite = HB2;
        }
        if (playerController.playerHealth == 1)
        {
            GetComponent<SpriteRenderer>().sprite = HB1;
        }

        if (playerController.playerHealth == 0)
        {
            GetComponent<SpriteRenderer>().sprite = HB0;
        }


        transform.position = toFollow.position - offset;
            transform.rotation = toFollow.rotation;
        

        //  if (Input.GetKeyDown(KeyCode.N))
        //   {
        //      GetComponent<SpriteRenderer>().sprite = HB2;
        //  }
    }
}
