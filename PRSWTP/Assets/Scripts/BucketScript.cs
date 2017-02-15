using UnityEngine;
using System.Collections;

public class BucketScript : MonoBehaviour {

    public GameObject charge;

    public void activate()
    {
        Rigidbody bucketRb = GetComponent<Rigidbody>();
        bucketRb.constraints = RigidbodyConstraints.None;
        bucketRb.AddForce(-transform.right * 3);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "L1 P1")
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Vector3 thisPos = this.gameObject.GetComponent<Transform>().position;
            Vector3 pos = new Vector3(thisPos.x + 3.23f, thisPos.y - 1, thisPos.z);
            Quaternion rot = Quaternion.Euler(0, 0, 0);
            Instantiate(charge, pos, rot);
        }
    }

    public void reset()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
