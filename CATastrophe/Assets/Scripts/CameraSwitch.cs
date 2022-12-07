using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public GameObject cam1;
    public GameObject cam2;
    public Teleport teleport;


    // Start is called before the first frame update
    
    
    void Start()
    {
         cam1.SetActive(true);
         cam2.SetActive(false);
        teleport.GetComponent<Teleport>();
    }

    // Update is called once per frame
    void Update()
    {

       

        if (teleport.level2Cutscene == true)
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
        }
    }

    public void SetCamera1()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
    }

    public void SetCamera2()
    {
        teleport.GetComponent<Teleport>();
      //  cam1.SetActive(false);
        cam2.SetActive(true);
    }
}
