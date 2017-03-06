using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBarDisplay : MonoBehaviour {

    public GameObject text;
    public GameObject player;
    private int currCharges;
    private int fullCharges;
    private float originalWidth;

    //(0, -3.3/2)
    //(3.3, 0)
    //x = -(3.3)/2+(1/2)*width

    // Use this for initialization
    void Start()
    {
        originalWidth = gameObject.transform.localScale.x;

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

        float currWidth = originalWidth * ((float)currCharges / (float)fullCharges);
        Debug.Log(currCharges + " " + fullCharges + " " + (float)currCharges/(float)fullCharges);
        gameObject.transform.localScale = new Vector3(currWidth, 0.27f, 0.1f);
        float currPos = -(3.3f / 2f) + (.5f) * currWidth;
        gameObject.transform.localPosition = new Vector3(currPos, 0, -0.1f);
    }
}
