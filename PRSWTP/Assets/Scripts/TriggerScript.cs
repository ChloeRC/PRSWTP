using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {

    public GameObject toBeTriggered;
    public string type;

    float numberTimesTriggered = 0f;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            numberTimesTriggered++;
            if (type == "Bucket") //Buckets can be triggered twice
            {
                toBeTriggered.GetComponent<BucketScript>().activate();
            }
            if (type == "Cone") //Cones can only be triggered once
            {
                toBeTriggered.GetComponent<ConeScript>().activate();
            }
        }
    }
}
