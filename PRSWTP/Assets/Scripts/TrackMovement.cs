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

    public GameObject player;
    public GameObject nonPlayerObjects;
    public GameObject bucket;
    public GameObject healthBar;
    public GameObject playerinfo;

	public int thisHealth;
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
		TimeTravelIndicator TimeTravelIndicator = GetComponent <TimeTravelIndicator>();
       PlayerInfo playerinfo = nonPlayerObjects.GetComponent<PlayerInfo>();

        int charge = PlayerScript.getCharges();
        //GameObject player2;
        if (charge == currLevelCharges()) //go back in time
        {
            ValueHolder.isPastSelfSpawning = true;
            //Vector3 currPos = this.transform.position;
            Instantiate(player);    //creates the previous self
            Debug.Log(player);
            PlayerScript.resetCharges();
            
            //The new, controllable one is "player"
            //The old, not-controllable, past self is "this"

			TimeTravelIndicator.setFlash();
            //player.GetComponent<TimeTravelIndicator>().setFlash(); //hahaha
            player.GetComponent<PlayerScript>().toggleControllable();

            //player.transform.rotation = Quaternion.Euler(0, 0, 0);

            HealthBarDisplay thisFullBar = this.transform.Find("DestroyOnTimeTravel").Find("HealthDisplay").Find("FullBar").GetComponent<HealthBarDisplay>();
            HealthBarDisplay playerFullBar = player.transform.Find("DestroyOnTimeTravel").Find("HealthDisplay").Find("FullBar").GetComponent<HealthBarDisplay>();

            playerFullBar.startValue = thisHealth;

            thisFullBar.UpdateText();
            playerFullBar.UpdateText();

            Transform destroyOnTimeTravel = this.transform.Find("DestroyOnTimeTravel");
            Destroy(destroyOnTimeTravel.gameObject);

            Resetter resetter = nonPlayerObjects.GetComponent<Resetter>();
            resetter.reset = true;
            bucket.GetComponent<BucketScript>().reset();

            //Debug.Log("Count before marker: " + locations.Count);
			//Debug.Log ("Key: " + key);
            locations.Add(key, MARKER);
            key++;
            player2Exists = true;

			thisHealth = player.GetComponent<PlayerScript>().health;            
            playerinfo.setHealth(thisHealth);

            //player.transform.position = currPos;
            //this.transform.position = currPos;
            Debug.Log(player.transform.position);
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
            currLevel++;



            int i = 0;
            while (locations.Count > 0)
            {
                locations.Remove(i);
                //i++;
                //Debug.Log("Count when removing: " + locations.Count);
            }
            key = 0;
            player2Exists = false;
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

	public int getThisHealth() {
		return thisHealth;
	}
}
