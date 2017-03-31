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

		//storeCurrWidth ();
		//storeCurrPos ();
		//setCurrWidth();
			
        //base.full = UpdateValue();
		//base.originalWidth = 1.5f;
		base.full = startValue;
        //Sometimes (when the first player exists), I want curr to be equal to full.
        //Other times (when the player travels back in time), I want curr to be left unchanged. How?
        //base.curr = base.full;
        //ORIG: base.curr = startValue;

		base.curr = UpdateValue ();
        base.originalText = "Health: ";
        base.Start();

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

        //Debug.Log(base.text.GetComponent<TextMesh>().text);
//
//		Debug.Log ("---------------------------");
//		Debug.Log ("Curr: " + base.curr);
//		Debug.Log ("Full: " + base.full);
//
//
		Debug.Log ("currWidth " + base.currWidth);
		Debug.Log ("currPos " + base.currPos);
//		Debug.Log ("originalWidth: " + base.originalWidth);
		Debug.Log ("---------------------------");
    }

	public void storeCurrWidth() 
	{
		PlayerInfo info = nonPlayerObjects.GetComponent<PlayerInfo>();
		info.setCurrWidth(base.currWidth);
	}

	public void storeCurrPos() 
	{
		PlayerInfo info = nonPlayerObjects.GetComponent<PlayerInfo>();
		info.setCurrWidth(base.currPos);
	}

	public void setCurrWidth() {
		PlayerInfo info = nonPlayerObjects.GetComponent<PlayerInfo>();
		base.currWidth = info.getCurrWidth();
	}

	public void setCurrPos() {
		PlayerInfo info = nonPlayerObjects.GetComponent<PlayerInfo>();
		base.currPos = info.getCurrPos();
	}
}
