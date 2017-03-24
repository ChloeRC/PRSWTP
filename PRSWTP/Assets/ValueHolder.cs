using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueHolder : MonoBehaviour {

    public static int checkpointNumber;
    public static int currentCheckpoint = 0;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
