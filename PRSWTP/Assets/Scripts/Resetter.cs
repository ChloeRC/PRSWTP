using UnityEngine;
using System.Collections;

public class Resetter : MonoBehaviour {

    private Hashtable positions;
    private Hashtable rotations;

    public bool reset;

	// Use this for initialization
	void Start () {
        positions = new Hashtable();
        rotations = new Hashtable();

        reset = false;
        foreach (Transform child in transform)
        {
            positions.Add(child.name, child.position);
            rotations.Add(child.name, child.rotation);
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
                //Debug.Log(positions[child.name]);
                child.position = (Vector3) positions[child.name];
                child.rotation = (Quaternion) rotations[child.name];
            }
        }
	}
}
