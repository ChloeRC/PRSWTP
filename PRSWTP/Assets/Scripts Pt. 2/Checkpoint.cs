using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class Checkpoint : MonoBehaviour {

    public int number;
    public GameObject timer;
    public GameObject player;

    void Start()
    {
        
    } 

    public void lightOn()
    {
        transform.GetChild(1).GetComponent<Light>().intensity = 8;
        timer.GetComponent<Timer>().reset();
        player.GetComponent<TrackMovement>().destroyClone();
    }
}
