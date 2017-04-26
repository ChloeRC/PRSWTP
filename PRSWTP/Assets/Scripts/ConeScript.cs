using UnityEngine;
using System.Collections;

public class ConeScript : MonoBehaviour {

    public bool falling = false;

	public void activate()
    {
        Debug.Log("*quiet crunch*");
		StartCoroutine (Drop ());
    }

	IEnumerator Drop() {
        falling = true;
		yield return new WaitForSeconds (.25f);
        GetComponent<Rigidbody>().useGravity = true;
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" && falling)
        {
            col.gameObject.GetComponent<PlayerScript>().kill();
        }
        if (col.gameObject.layer == 8 && falling) //ground
        {
            Debug.Log("crunch - BOOM");
            falling = false;
            freeze();
        }
    }

    void freeze()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
