using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarDisplay : FillBar {

    public GameObject player;
	public GameObject nonPlayerObjects;
	public PlayerInfo info; //stores information
    public int startValue = 3;

    // Use this for initialization
    new void Start () {
		base.full = startValue;
		base.curr = UpdateValue ();
        base.originalText = "Health: ";
        base.Start();

        originalWidth = 3.3f;

        //Debug.Log("START " + base.text.GetComponent<TextMesh>().text);
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
