using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarDisplay : FillBar {

    public GameObject player;
    public int startValue = 3;

    // Use this for initialization
    new void Start () {
        //base.full = UpdateValue();
		base.full = 3;
        //Sometimes (when the first player exists), I want curr to be equal to full.
        //Other times (when the player travels back in time), I want curr to be left unchanged. How?
        //base.curr = base.full;
        //ORIG: base.curr = startValue;

		base.curr = UpdateValue ();
        base.originalText = "Health: ";
        base.Start();

        Debug.Log("START " + base.text.GetComponent<TextMesh>().text);
    }

    public int UpdateValue()
    {
        return player.GetComponent<PlayerScript>().health;
    }

    public new void UpdateText()
    {
        base.curr = UpdateValue();
        base.UpdateText();

        Debug.Log(base.text.GetComponent<TextMesh>().text);
    }
}
