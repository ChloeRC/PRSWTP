using UnityEngine;
using System.Collections;

public class BucketScript : MonoBehaviour {

    public GameObject charge;

    private bool newCharge = true;

    private bool isActivated = false;

    void Start()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    public void activate()
    {
        Debug.Log("Pardon me, are you Aaron Burr, sir?");
        isActivated = true;
        Rigidbody bucketRb = GetComponent<Rigidbody>();
        bucketRb.constraints = RigidbodyConstraints.None;
        bucketRb.AddForce(-transform.right * 8);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "L1 P1")
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Vector3 thisPos = this.gameObject.GetComponent<Transform>().position;
            Vector3 pos = new Vector3(thisPos.x + 3.23f, thisPos.y - 1, thisPos.z);
            Quaternion rot = Quaternion.Euler(0, 0, 0);
            if (newCharge)
            {
                Instantiate(charge, pos, rot);
                newCharge = false;
            }
        }
    }

    public void reset()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    void Update()
    {
        if (!isActivated)
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
