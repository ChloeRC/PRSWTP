using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerScript : MonoBehaviour {

    public GameObject toBeTriggered;
    public string type;

    private int coneActivatedTimes = 0;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("crash bang boom");

            if (type == "Bucket")
            {
                toBeTriggered.GetComponent<BucketScript>().activate();
            }
            if (type == "Cone" && coneActivatedTimes == 0)
            {
                coneActivatedTimes++;
                toBeTriggered.GetComponent<ConeScript>().activate();
            }
            if (type == "Parrot")
            {
                toBeTriggered.GetComponent<ParrotScript>().activate();
            }
            if (type == "EndGame")
            {
                SceneManager.LoadScene("EndGame");
            }
        }
    }
}
