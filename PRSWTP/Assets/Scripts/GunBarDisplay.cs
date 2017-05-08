using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBarDisplay : FillBar {

    public GameObject player;
    public GameObject nonPlayerObjects;
    public PlayerInfo info; //stores information
    private int startValue;

    // Use this for initialization
    new void Start()
    {
        startValue = player.GetComponent<PlayerScript>().shotCooldown;
        base.full = startValue;
        base.curr = UpdateValue();
        base.originalText = "Gun";
        base.originalWidth = 1f;
        base.useDivide = false;
        base.Start();
    }

    public float UpdateValue()
    {
        return player.GetComponent<PlayerScript>().hasShot;
    }

    public new void UpdateText()
    {
        base.curr = UpdateValue();
        base.UpdateText();
    }
}
