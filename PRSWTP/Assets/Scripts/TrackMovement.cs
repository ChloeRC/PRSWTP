using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class TrackMovement : MonoBehaviour {

    private Hashtable locations;
    public int framerate; //the length of a big frame
    public string full;
    public List<Int32> chargesPerLevel = new List<Int32>();
    public int currLevel = 0;
    private int frameNum = 0;
    private int key = 0;
    private int key2 = 0;
    public static readonly Vector3 MARKER = new Vector3(0f, -21f, 0f);
    public Vector3 spawnLocation;

    public GameObject clone;
    public GameObject nonPlayerObjects;
    public GameObject bucket;
    public GameObject healthBar;
    public GameObject chargeBarDisplay;
    public GameObject timer;

	public int thisHealth;

    private GameObject newClone;
	// Use this for initialization
	void Start () {
        parseFull();
        locations = new Hashtable();
	}
	
	// Update is called once per frame
	void Update () {
        if (frameNum % framerate == 0) //every big frame
        {
            locations.Add(key, transform.position); //Add the player's current location to the locations map
            key++; //The location's number
        }
        frameNum++;

        PlayerScript PlayerScript = GetComponent<PlayerScript>();
		TimeTravelIndicator TimeTravelIndicator = GetComponent <TimeTravelIndicator>();

        int charge = PlayerScript.getCharges();
        if (charge == currLevelCharges()) //go back in time
        {
            newClone = Instantiate(clone);

            PlayerScript.resetCharges();

			TimeTravelIndicator.setFlash();

            Resetter resetter = nonPlayerObjects.GetComponent<Resetter>(); //resets the location of everything
            resetter.reset = true;
            bucket.GetComponent<BucketScript>().reset(); //resets the bucket's status

            locations.Add(key, MARKER); //Adds a marker which indicates that the past self should be destroyed
            key++;

			thisHealth = this.GetComponent<PlayerScript>().health;
        }

        if ((newClone != null) && (frameNum % framerate == 0)) //every big frame
        {
            if (((Vector3)locations[key2] != MARKER))
            {
                newClone.GetComponent<CloneScript>().setPosition((Vector3)locations[key2]); //set the clone's location
                key2++;
            }
            else
            {
                Destroy(newClone);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Checkpoint")
        {
            currLevel = col.GetComponent<Checkpoint>().number;
            chargeBarDisplay.GetComponent<ChargeBarDisplay>().UpdateFull();
            int i = 0;
            while (locations.Count > 0)
            {
                locations.Remove(i);
                i++;
            }
            key = 0;
        }

    }

    void parseFull()
    {
        chargesPerLevel = new List<Int32>();

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

	public int getThisHealth() {
		return thisHealth;
	}
}
