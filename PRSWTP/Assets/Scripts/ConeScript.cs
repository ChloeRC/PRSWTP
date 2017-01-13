using UnityEngine;
using System.Collections;

public class ConeScript : MonoBehaviour {

	public void activate()
    {
		StartCoroutine (Drop ());
    }

	IEnumerator Drop() {
		yield return new WaitForSeconds (.25f);
		GetComponent<Rigidbody>().AddForce(-transform.up * 1000);
	}
}
