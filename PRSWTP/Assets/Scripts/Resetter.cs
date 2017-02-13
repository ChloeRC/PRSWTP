using UnityEngine;
using System.Collections;

public class Resetter : MonoBehaviour {

    private Hashtable objects;

    public bool reset;

	// Use this for initialization
	void Start () {
        objects = new Hashtable();
        reset = false;
        foreach (Transform child in transform)
        {
            objects.Add(child.name, child.position);
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (reset)
        {
            Debug.Log("reset");
            reset = false;
            foreach (Transform child in transform)
            {
                Debug.Log(child.name + " " + child.position);
                child.position = (Vector3) objects[child.name];
            }
        }
	}
}
