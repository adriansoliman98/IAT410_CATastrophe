using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{

    private Transform destination;
   public  CameraSwitch cameraSwitch;
    public Vector2 level2Spawn;
    public bool inlevel2, inlevel3, finalLevel;
    public bool level2Cutscene = false;

    public float distance = 5f;

    // Start is called before the first frame update
    void Start()
    {
       // if (level1 == true)
      //  {
       //     destination = GameObject.FindGameObjectWithTag("level2start").GetComponent<Transform>();
       // }
    }

    // Update is called once per frame
    void Update()
    {
      //  cameraSwitch.GetComponent<CameraSwitch>();
       // print(level2Cutscene);
      
    }

   void OnTriggerEnter2D(Collider2D other)
    {
        if (this.gameObject.tag == "Level1Exit" && other.CompareTag("Player"))
        {
            SceneManager.LoadScene(3, LoadSceneMode.Additive);
            destination = GameObject.FindGameObjectWithTag("level2start").GetComponent<Transform>();

           // cameraSwitch.SetCamera2();
            if (Vector2.Distance(transform.position,other.transform.position) > distance)
            {
                   other.transform.position = new Vector2 (destination.position.x, destination.position.y);

                   inlevel2 = true;
                   inlevel3 = false;
                //level2Cutscene = true;
            //    cameraSwitch.SetCamera2();


                // cameraSwitch.SetCamera2();
               // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
              
            }

           

        }

        if (this.gameObject.tag == "Level2Exit" && other.CompareTag("Player"))
        {
          //  SceneManager.LoadScene(4, LoadSceneMode.Additive);
            destination = GameObject.FindGameObjectWithTag("Level3Start").GetComponent<Transform>();
            if (Vector2.Distance(transform.position, other.transform.position) > distance)
            {
                other.transform.position = new Vector2(destination.position.x, destination.position.y);
                inlevel2 = false;
                inlevel3 = true;
              

            }

        }

        if (this.gameObject.tag == "Level3Exit" && other.CompareTag("Player"))
        {
            SceneManager.LoadScene(4, LoadSceneMode.Additive);
            destination = GameObject.FindGameObjectWithTag("BossStart").GetComponent<Transform>();
            if (Vector2.Distance(transform.position, other.transform.position) > distance)
            {
                other.transform.position = new Vector2(destination.position.x, destination.position.y);
                inlevel2 = false;
                inlevel3 = false;
                finalLevel = true;
               
            }

        }

        if (this.gameObject.tag == "FinalExit" && other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +3);
            // destination = GameObject.FindGameObjectWithTag("BossStart").GetComponent<Transform>();
            //  if (Vector2.Distance(transform.position, other.transform.position) > distance)
            // {
            //     other.transform.position = new Vector2(destination.position.x, destination.position.y);
            //     inlevel2 = false;
            //     inlevel3 = false;
            //     finalLevel = true;

        }



        
    }
}
