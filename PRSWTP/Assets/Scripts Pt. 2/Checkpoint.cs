using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public int number;

    private bool isActivated;

    void Start()
    {
        isActivated = false;
    }

    public void activate()
    {
        isActivated = true;
    }

    public bool activated()
    {
        return isActivated;
    }
}
