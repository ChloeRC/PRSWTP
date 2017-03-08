using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunReloadDisplay : MonoBehaviour {
    public GameObject player;
    int reloadTime;

	// Use this for initialization
	void Start () {
        reloadTime = 0;	
	}
	
	// Update is called once per frame
	void Update () {
        getReloadTime();
	}

    void getReloadTime()
    {
        reloadTime = 4 - player.GetComponent<BulletScript>().getTime();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(200, 50, 50, 50), "Gun time: " + reloadTime);
        //sorry just gotta test things
    }
}
