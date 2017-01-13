using UnityEngine;
using System.Collections;

public class ConeScript : MonoBehaviour {

	public void activate()
    {
		StartCoroutine (Drop ());
    }

	IEnumerator Drop() {
		yield return new WaitForSeconds (5);
		GetComponent<Rigidbody>().AddForce(-transform.up * 10000);
	}
}
