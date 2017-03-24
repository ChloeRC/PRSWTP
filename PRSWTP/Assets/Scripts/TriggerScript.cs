using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {

    public GameObject toBeTriggered;
    public string type;

    private int coneActivatedTimes = 0;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (type == "Bucket")
            {
                toBeTriggered.GetComponent<BucketScript>().activate();
            }
            if (type == "Cone" && coneActivatedTimes == 0)
            {
                toBeTriggered.GetComponent<ConeScript>().activate();
                coneActivatedTimes++;
            }
            if (type == "Parrot")
            {
                toBeTriggered.GetComponent<ParrotScript>().activate();
            }
        }
    }
}
