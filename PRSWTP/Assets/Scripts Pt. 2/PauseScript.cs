using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {
    GameObject[] pauseObjects;
    GameObject player;
    private bool pause;

	// Use this for initialization
	void Start () {
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
	}
	
	// Update is called once per frame
	void Update () {
        pause = player.GetComponent<PlayerScript>().getPause();
        Debug.Log(pause);

        if (pause)
        {
            showPaused();
        }
        if (!pause)
        {
            hidePaused();
        }
	}

    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides paused objects
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }


}
