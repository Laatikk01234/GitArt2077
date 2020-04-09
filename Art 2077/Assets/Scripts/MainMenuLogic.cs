using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public GameObject credits;
    private bool canClick;
    private void Update()
    {
        if (canClick == false)
        {
            if (Input.GetMouseButtonUp(0))
            {
                credits.SetActive(false);
            }
        }
        if (credits.activeSelf)
        {
            canClick = false;
        }
        else
        {
            canClick = true;
        }


    }
    public void Play()
    {
        if (canClick)
        {
            SceneManager.LoadScene("Main Game");
        }
    }

    public void Credits()
    {
        if (canClick)
        {
            credits.SetActive(true);
        }
    }

    public void Quit()
    {
        if (canClick)
        {
            Application.Quit();
        }
    }
}
