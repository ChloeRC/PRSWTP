using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarDisplay : FillBar {

    public GameObject player;

    // Use this for initialization
    new void Start () {
        base.full = UpdateValue();
        //Sometimes (when the first player exists), I want curr to be equal to full.
        //Other times (when the player travels back in time), I want curr to be left unchanged. How?
        base.curr = base.full;
        Debug.Log("full: " + base.full + " curr: " + base.curr);
        base.originalText = "Health: ";
        base.Start();
    }
    
    public int UpdateValue()
    {
        return player.GetComponent<PlayerScript>().health;
    }

    public new void UpdateText()
    {
        base.curr = UpdateValue();
        base.UpdateText();
    }
}
