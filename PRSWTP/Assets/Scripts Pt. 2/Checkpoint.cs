using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public float targetX;
    public float targetY;
    public float targetZ;

    public bool isActivated;

    void Start()
    {
        isActivated = false;
    }
}
