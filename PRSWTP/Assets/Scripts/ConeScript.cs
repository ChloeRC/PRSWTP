using UnityEngine;
using System.Collections;

public class ConeScript : MonoBehaviour {

	public void activate()
    {
        int n = 0;
        while (n < 1000) { n++; }
        GetComponent<Rigidbody>().AddForce(-transform.up * 10000);
    }

}
