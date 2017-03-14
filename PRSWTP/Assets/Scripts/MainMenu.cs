using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public string goTo;

    void Start()
    {
    }

    //Starts the first level after new game starts
    void OnMouseDown()
    {
        SceneManager.LoadScene(goTo);
    }
}
