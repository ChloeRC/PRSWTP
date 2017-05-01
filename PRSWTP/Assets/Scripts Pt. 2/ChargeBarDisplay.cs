using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBarDisplay : FillBar {

    //(0, -3.3/2)
    //(3.3, 0)
    //x = -(3.3)/2+(1/2)*width

    public GameObject player;
    TrackMovement tm;

    // Use this for initialization
    new void Start()
    {
        tm = player.GetComponent<TrackMovement>();
        base.full = tm.currLevelCharges();
        base.curr = 0;

        base.originalText = "Charges: ";
        base.originalWidth = 3.3f;
        base.useDivide = true;
        base.Start();
    }

    public int UpdateValue()
    {
        return player.GetComponent<PlayerScript>().getCharges();
    }

    public new void UpdateText()
    {
        base.curr = UpdateValue();
        base.UpdateText();
    }

    public void UpdateFull()
    {
        base.full = tm.currLevelCharges();
    }
}
