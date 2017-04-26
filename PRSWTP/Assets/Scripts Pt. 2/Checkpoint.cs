using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public int number;

    public void lightOn()
    {
        transform.GetChild(1).GetComponent<Light>().intensity = 8;
    }

    public void lightOff()
    {
        transform.GetChild(1).GetComponent<Light>().intensity = 0;
    }
}
