using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    
    public void Play()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void Credits()
    {
        // or popup ui?
        SceneManager.LoadScene("Credits");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
