using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public string goTo;
    public int checkpointNumber;

    void Start()
    {

    }

    //Starts the first level after new game starts
    void OnMouseDown()
    {
        if (checkpointNumber != 0)
        {
            ValueHolder.checkpointNumber = this.checkpointNumber;
        }

        SceneManager.LoadScene(goTo);
    }
}
