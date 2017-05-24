using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerScript : MonoBehaviour {

    public GameObject[] toBeTriggered;
    public string type;

    private int coneActivatedTimes = 0;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Debug.Log("crash bang boom");
            if (type == "Bucket")
            {
                foreach (GameObject toBeTrig in toBeTriggered)
                {
                    toBeTrig.GetComponent<BucketScript>().activate();
                }
            }
            if (type == "Cone" && coneActivatedTimes == 0)
            {
                coneActivatedTimes++;
                toBeTriggered[0].GetComponent<ConeScript>().activate();
            }
            if (type == "Parrot")
            {
                toBeTriggered[0].GetComponent<ParrotScript>().activate();
            }
            if (type == "EndGame")
            {
                SceneManager.LoadScene("EndGame");
            }
        }
        else if (col.gameObject.tag == "Clone")
        {
            if (type == "Bucket")
            {
                toBeTriggered[1].GetComponent<BucketScript>().activate();
            }
        }
    }
}
