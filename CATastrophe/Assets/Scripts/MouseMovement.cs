using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour


{

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(21f*25, 21f * 25));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
    }
}
