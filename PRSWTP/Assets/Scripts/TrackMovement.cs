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
    public static readonly CloneLocation DESTROY_CLONE = new CloneLocation(
        new Vector3(0, -200, 0),
        new Vector3(0, 0, 0),
        false,
        false
        );

    public GameObject clone;
    public GameObject nonPlayerObjects;
    public GameObject bucket;
    public GameObject healthBar;
    public GameObject chargeBarDisplay;
    public GameObject timer;
    public GameObject playerRotation;

    private GameObject newClone;

	// Use this for initialization
	void Start () {
        parseFull();
        locations = new Hashtable();
	}
	
	// Update is called once per frame
	void Update () {
        PlayerScript PlayerScript = GetComponent<PlayerScript>();
        TimeTravelIndicator TimeTravelIndicator = GetComponent<TimeTravelIndicator>();

        if (frameNum % framerate == 0) //every big frame, record the player's actions
        {
            bool didSword = PlayerScript.didSword;
            bool didShoot = PlayerScript.didShoot;

            CloneLocation toAdd = new CloneLocation(transform.position, 
                    playerRotation.transform.rotation.eulerAngles, didSword, didShoot);

            if (didShoot) { PlayerScript.didShoot = false; }
            if (didSword) { PlayerScript.didSword = false; }

            locations.Add(key, toAdd); //Add the player's current location to the locations map
            key++; //The location's number
        }

        frameNum++;

        int charge = PlayerScript.getCharges();
        if (charge == currLevelCharges()) //go back in time
        {
            newClone = Instantiate(clone); //create the clone

            PlayerScript.resetCharges(); //set charges to 0

			TimeTravelIndicator.setFlash("white"); //flash the screen

            Resetter resetter = nonPlayerObjects.GetComponent<Resetter>(); //resets the location of everything
            resetter.reset = true;
            bucket.GetComponent<BucketScript>().reset(); //resets the bucket's status

            locations.Add(key, DESTROY_CLONE); //Adds a marker - the past self should be destroyed here
            key++;
        }

        if ((newClone != null) && (frameNum % framerate == 0)) //every big frame, update the clone
        {
            CloneLocation toSet = (CloneLocation)locations[key2];
            CloneScript cloneScript = newClone.GetComponent<CloneScript>();
            if (!PlayerScript.getPause()) {  //Pauses the clone
                cloneScript.setLocation(toSet); //set the clone's location
                key2++;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Checkpoint")
        {
            GetComponent<AudioSource>().Play();
            currLevel = col.GetComponent<Checkpoint>().number;
            chargeBarDisplay.GetComponent<ChargeBarDisplay>().UpdateFull();
            locations = new Hashtable();
            key = 0;
            key2 = 0;
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
}
