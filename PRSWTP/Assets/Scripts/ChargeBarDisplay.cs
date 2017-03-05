using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBarDisplay : MonoBehaviour {

    public GameObject text;
    public GameObject player;
    private int currCharges;
    private int fullCharges;

    // Use this for initialization
    void Start()
    {
        currCharges = 0;

        TrackMovement tm = player.GetComponent<TrackMovement>();
        fullCharges = tm.currLevelCharges();

        UpdateCharges();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCharges()
    {
        currCharges = player.GetComponent<PlayerScript>().getCharges();
        text.GetComponent<TextMesh>().text = "Charges: " + currCharges + " / " + fullCharges;
    }
}
