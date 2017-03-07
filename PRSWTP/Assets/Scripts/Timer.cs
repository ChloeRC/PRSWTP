
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Timer : MonoBehaviour {
    public GameObject player;
    public GameObject timer;
    private double currentTime;
    private double timeTravel;

	// Use this for initialization
	void Start () {
        currentTime = player.GetComponent<PlayerScript>().timer;
        TrackMovement trackm = player.GetComponent<TrackMovement>();

        Debug.Log(currentTime);
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        PlayerScript PlayerScript = GetComponent<PlayerScript>();
        TimeTravelIndicator TimeTravelIndicator = GetComponent<TimeTravelIndicator>();
        TrackMovement TrackMovement = GetComponent<TrackMovement>();
        if ()
        currentTime +=
               time += Time.deltaTime;
        //despawn timer
        if (time > .4)
        {
            timer.kill();
        }

    }
}
/*
public class StartCountdown : MonoBehaviour
{
    int time, a;
    float x;
    public bool count;
5.     public string timeDisp;

    void Start()
    {
        time = 3;
        count = false;
        10.     }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (count)
        {
            15.timeDisp = time.ToString();
            GameObject.Find("StartCounter").GetComponent<Text>().text = timeDisp;
            x += Time.deltaTime;
            a = (int)x;
            print(a);
            20.             switch (a)
            {
                case 0: GameObject.Find("StartCounter").GetComponent<Text>().text = "3"; break;
                case 1: GameObject.Find("StartCounter").GetComponent<Text>().text = "2"; break;
                case 2: GameObject.Find("StartCounter").GetComponent<Text>().text = "1"; break;
                case 3:
                    GameObject.Find("StartCounter").GetComponent<Text>().enabled = false;
                    25.count = false; break;
            }
        }
    }
}
*/