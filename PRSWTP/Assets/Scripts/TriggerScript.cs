using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {

    public GameObject toBeTriggered;
    public string type;

    bool hasBeenTriggered = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<PlayerScript>().getControllable() && !hasBeenTriggered)
        {
            //Debug.Log("augh");
            hasBeenTriggered = true;
            if (type == "Bucket")
            {
                Debug.Log("auuuugh");
                toBeTriggered.GetComponent<BucketScript>().activate();
            }
            if (type == "Cone")
            {
                toBeTriggered.GetComponent<ConeScript>().activate();
            }
        }
    }
}
