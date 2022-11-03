using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public int Respawn;
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

     

      //  if (collision.gameObject.tag == "Enemy")
      //  {
          //  SceneManager.LoadScene(Respawn);
      //    Destroy(gameObject);
    //    }
    }




}
