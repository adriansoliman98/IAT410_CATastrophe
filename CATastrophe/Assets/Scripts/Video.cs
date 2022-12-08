using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Video : MonoBehaviour
{
    //video.loopPointReached += EndReached;
    public float timer;
    Video video;
   // public GameObject videoPlayer;
    public int timeToStop;
    //public Teleport teleport;
   // private VideoPlayer video;
    
    // Start is called before the first frame update
    void Start()
    {
        // if (this.gameObject.tag == "3rdLevelCutscene" && this.gameObject.tag == "2ndLevelCutscene" &&
        //      this.gameObject.tag == "4thLevelCutscene") { 
        //   video
        // 
      //  video = GetComponent<VideoPlayer>();    
      //  videoPlayer.SetActive(true);
       // video.Pause();
       // teleport = new Teleport();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //Scene currentScene = SceneManager.GetActiveScene();
        video = GetComponent<Video>();
       // string sceneName = currentScene.name;
        if (timer <= 0)
        {
            if (video.tag == "3rdLevelCutscene")
            {
                SceneManager.UnloadSceneAsync(4);
            }
            if (video.tag == "4thLevelCutscene")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -5);
            }
            if (video.tag == "2ndLevelCutscene")
            {
                SceneManager.UnloadSceneAsync(3);
            }

            if (video.tag == "IntroCutscene")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
            }
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        //  print(teleport.level2Cutscene);
        if (this.gameObject.tag == "IntroCutscene")
        {
           // timer -= Time.deltaTime;

           // if (timer <= 0)
          //  {
                //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
           //     SceneManager.UnloadSceneAsync(4);
          //  }
        }

        if (this.gameObject.tag == "2ndLevelCutscene")
        {
         
        }
        if (this.gameObject.tag == "3rdLevelCutscene")
        {


        }

        if (this.gameObject.tag == "4thLevelCutscene")
        {


        }

    }

   

   
}
