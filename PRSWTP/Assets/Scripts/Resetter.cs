using UnityEngine;
using System.Collections;

public class Resetter : MonoBehaviour {

    private Hashtable objects;

	// Use this for initialization
	void Start () {
        objects = new Hashtable();
        foreach (Transform child in transform)
        {
            objects.Add(child.name, child.position);
        }
        Debug.Log(objects);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
