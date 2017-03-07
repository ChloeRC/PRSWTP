using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarDisplay : FillBar {

    public GameObject player;

    // Use this for initialization
    new void Start () {
        base.full = UpdateValue();
        base.curr = base.full;
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
