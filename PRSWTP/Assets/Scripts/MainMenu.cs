using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{

    public GameObject NewGame;
    public GameObject LoadGame;

    void Start()
    {	
    }

    // Update is called once per frame
    //Starts the first level after new game starts
    void Update()
    {
    }

    void StartGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("FirstLevel"); // LoadSceneMode.Additive);        
        }

    }
}

