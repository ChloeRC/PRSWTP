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
        fullCharges = player.GetComponent<TrackMovement>().currLevelCharges();
        Debug.Log(fullCharges);

        UpdateCharges();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCharges()
    {
        Debug.Log("update");
        currCharges = player.GetComponent<PlayerScript>().getCharges();
        text.GetComponent<TextMesh>().text = "Charges: " + currCharges + " / " + fullCharges;
    }
}
