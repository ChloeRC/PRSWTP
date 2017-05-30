using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class Checkpoint : MonoBehaviour {

    public int number;
    void Start()
    {
        
    } 

    public void lightOn()
    {
        transform.GetChild(1).GetComponent<Light>().intensity = 8;
    }

    public void update()
    {
        GetComponent<AudioSource>().Play();
        transform.GetChild(1).GetComponent<Light>().intensity = 0;
    }
}
