using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarDisplay : FillBar {

    public GameObject player;

    // Use this for initialization
    void Start () {
        base.originalText = "Health: ";
    }
    
    new void UpdateValue()
    {
        curr = player.GetComponent<PlayerScript>().health;
    }
}
