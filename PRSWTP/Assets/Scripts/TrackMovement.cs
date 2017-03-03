using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class TrackMovement : MonoBehaviour {

    private Hashtable locations;
    public int framerate;
    public string full;
    public List<Int32> chargesPerLevel = new List<Int32>();
    public int currLevel = 0;
    private int test = 0;
    private int key = 0;
    private bool player2Exists = false;
    private int spot2 = 0;
    public static readonly Vector3 MARKER = new Vector3(0f, -21f, 0f);

    public GameObject player;
    public GameObject nonPlayerObjects;
    public GameObject bucket;

	// Use this for initialization
	void Start () {
        locations = new Hashtable();
        parseFull();
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
        //GameObject player2;
        if (charge == currLevelCharges()) //go back in time
        {
            Instantiate(player);
            Debug.Log(player);
            PlayerScript.resetCharges();
            player.GetComponent<TimeTravelIndicator>().setFlash(); //hahaha
            player.GetComponent<PlayerScript>().toggleControllable();
            player.transform.position = new Vector3(0, 3, 0);
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            //gets the children that are attached to the player copy and destroys them
            //for (var i = 0; i < transform.childCount; i++)
                //Destroy(transform.GetChild(i).gameObject);

            Transform camera = transform.Find("Camera");
            Destroy(camera.gameObject);
            Transform mapCamera = transform.Find("Map Camera");
            Destroy(mapCamera.gameObject);

            Resetter resetter = nonPlayerObjects.GetComponent<Resetter>();
            resetter.reset = true;
            bucket.GetComponent<BucketScript>().reset();

            //Debug.Log("Count before marker: " + locations.Count);
            locations.Add(key, MARKER);
            key++;
            player2Exists = true;
        }
        if (player2Exists && test % framerate == 0)
        {
            //Debug.Log("Spot2:" + spot2);
            player.GetComponent<PlayerScript>().setPosition((Vector3)locations[spot2]);
            spot2++;
        }

        
    }

    void OnTriggerEnter(Collider col)
    {
        bool controllable = player.transform.gameObject.GetComponent<PlayerScript>().getControllable();
        if (controllable && col.gameObject.tag == "Checkpoint")
        {

            int i = 0;
            while (locations.Count > 0)
            {
                locations.Remove(i);
                i++;
                //Debug.Log("Count when removing: " + locations.Count);
            }
            key = 0;
        }
    }

    void parseFull()
    {
        string[] chargesPerLevelStr = full.Split(' ');

        foreach (string num in chargesPerLevelStr)
        {
            chargesPerLevel.Add(Convert.ToInt32(num));
        }
    }

    /**
     * Returns the number of charges for the current level.
     * @return charges The number of charges in the given level.
     */
    public int currLevelCharges()
    {
        parseFull();
        return chargesPerLevel[currLevel];
    }
}
