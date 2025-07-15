using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControler : MonoBehaviour
{
     private bool isDoubleSpeed = false;

     private int speedMultiplier = 2; 


    public void BacktoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToTheGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void Turbo()
    {
        if (isDoubleSpeed)
        {
            Time.timeScale = 1f; // Velocidad normal
            isDoubleSpeed = false;
            Debug.Log("Normal Speed on");
        }
        else
        {
            Time.timeScale = speedMultiplier; // Velocidad doble
            isDoubleSpeed = true;
            Debug.Log("Speed x2");
        }
    }
}
