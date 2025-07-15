using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControler : MonoBehaviour
{
     private bool isDoubleSpeed = false;
     private bool isPused = false;

     private float speedMultiplier = 4f; 


    public void BacktoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToLevel1()
    {
        SceneManager.LoadScene(1);
    }

     public void GoToLevel2()
    {
        SceneManager.LoadScene(2);
    }


    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

     public void Pause()
    {
        if (isPused)
        {
            if (isDoubleSpeed == true)
            {
                Time.timeScale = speedMultiplier;
            }
            else
            {
                Time.timeScale = 1f;
            }
            isPused = false;
            Debug.Log("Game on");
        }
        else
        {
            Time.timeScale = 0f;
            isPused = true;
            Debug.Log("Pause");
        }
    }

    public void Turbo()
    {
        if (isPused == false)
        {
            if (isDoubleSpeed)
            {
                Time.timeScale = 1f;
                isDoubleSpeed = false;
                Debug.Log("Normal Speed on");
            }
            else
            {
                Time.timeScale = speedMultiplier;
                isDoubleSpeed = true;
                Debug.Log("Speed x2");
            }

        }
        else
        {
            return;
        }
       
    }
}
