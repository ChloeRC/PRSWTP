using UnityEngine;
using System.Collections;

public class TrackMovement : MonoBehaviour {

    private Hashtable locations;
    public int framerate;
    private int test = 0;
    private int key = 0;

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
            foreach (DictionaryEntry spot in locations)
            {
                Debug.Log(spot.Key + " " + spot.Value);
            }
            key++;
        }
        test++;
        PlayerScript PlayerScript = GetComponent<PlayerScript>();
        int charge = PlayerScript.getCharges();
        if (charge == 2)
        {
            Instantiate(player);
            resetCharges();
        }
	}
}
