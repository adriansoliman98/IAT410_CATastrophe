using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        
    }

    private void Update()
    {
      
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {      
        
            SceneManager.UnloadSceneAsync(3);
     
       
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Skip3rdCutscene()
    {
        SceneManager.UnloadSceneAsync(4);
    }

public void Skip4thCutscene()
{
    SceneManager.UnloadSceneAsync(5);
}
}
