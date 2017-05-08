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
    public Vector3 spawnLocation;

    private float gameTicks; //this keeps track of time since time travel
    private float SORRY; //stores time used since time travel
	//really problematic ^

    public GameObject clone;
    public GameObject player;
    public GameObject nonPlayerObjects;
    public GameObject bucket;
    public GameObject healthBar;
    public GameObject chargeBarDisplay;
    public GameObject playerinfo;

	public int thisHealth;

    private GameObject newClone;
	// Use this for initialization
	void Start () {
        parseFull();
        locations = new Hashtable();
        gameTicks = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        gameTicks += Time.deltaTime;
        if (test % framerate == 0)
        {
            locations.Add(key, transform.position);
            key++;
        }
        test++;
        PlayerScript PlayerScript = GetComponent<PlayerScript>();
		TimeTravelIndicator TimeTravelIndicator = GetComponent <TimeTravelIndicator>();
        PlayerInfo playerinfo = nonPlayerObjects.GetComponent<PlayerInfo>();

        int charge = PlayerScript.getCharges();
        if (charge == currLevelCharges()) //go back in time
        {
            SORRY = gameTicks;
            gameTicks = 0.0f;

            newClone = Instantiate(clone);
            Destroy(newClone, SORRY);

            PlayerScript.resetCharges();

			TimeTravelIndicator.setFlash();

            HealthBarDisplay thisFullBar = this.transform.Find("DestroyOnTimeTravel").Find("HealthDisplay").Find("FullBar").GetComponent<HealthBarDisplay>();
            HealthBarDisplay playerFullBar = player.transform.Find("DestroyOnTimeTravel").Find("HealthDisplay").Find("FullBar").GetComponent<HealthBarDisplay>();

            playerFullBar.startValue = thisHealth;

            thisFullBar.UpdateText();
            playerFullBar.UpdateText();

            Resetter resetter = nonPlayerObjects.GetComponent<Resetter>();
            resetter.reset = true;
            bucket.GetComponent<BucketScript>().reset();

            locations.Add(key, MARKER);
            key++;
            player2Exists = true;

			thisHealth = player.GetComponent<PlayerScript>().health;            
            playerinfo.setHealth(thisHealth);
        }

        if ((newClone != null) && (test % framerate == 0))
        {
            newClone.GetComponent<CloneScript>().setPosition((Vector3)locations[spot2]); 
            spot2++;
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
