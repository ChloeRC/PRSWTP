using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TrackMovement : MonoBehaviour {

    private Hashtable locations;
    public int framerate;
    public int full;
    private int test = 0;
    private int key = 0;
    private bool player2Exists = false;
    private int spot2 = 0;
    public static readonly Vector3 MARKER = new Vector3(0f, -21f, 0f);

    public GameObject player;

	// Use this for initialization
	void Start () {
        locations = new Hashtable();
	}
	
	// Update is called once per frame
	void Update () {
	    
        if (test % framerate == 0)
        {
            locations.Add(key, transform.position);
            key++;
        }
        test++;
        PlayerScript PlayerScript = GetComponent<PlayerScript>();
        int charge = PlayerScript.getCharges();
        if (charge == full)
        {
            Instantiate(player);
            PlayerScript.resetCharges();
            player.GetComponent<PlayerScript>().toggleControllable();
            player.transform.position = new Vector3(0, 3, 0);
            //gets the children attached to the player copy and destroys them
            for (var i = 0; i < transform.childCount; i++)
                Destroy(transform.GetChild(i).gameObject);

            //Resetter Resetter = GetComponent<Resetter>();
            //Resetter.reset = true;

            locations.Add(key, MARKER);
            key++;
            player2Exists = true;
        }
        if (player2Exists && test % framerate == 0)
        {
            player.GetComponent<PlayerScript>().setPosition((Vector3)locations[spot2]);
            spot2++;
        }
	}
}
