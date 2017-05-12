using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotScript : MonoBehaviour {

    public float stop;

    private Rigidbody rb;

    private bool activated;
    private bool f;
    public double timer;

    private static readonly string NEXT = "next";

    public string[] messages;
    private int currMessage;

    // Use this for initialization
    void Start () {
        activated = false;
        f = true;

        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;

        timer = 0;

        //gameObject.GetComponent<MeshRenderer>().enabled = false;
        foreach (Transform child in transform)
        {
            if (child.GetComponent<MeshRenderer>() != null)
            {
                child.GetComponent<MeshRenderer>().enabled = false;
            }
        }

        currMessage = 0;
        for (int i = 0; i < messages.Length; i++)
        {
            messages[i] = messages[i].Replace("\\n", "\n");
        }
        transform.Find("Text").GetComponent<TextMesh>().text = messages[currMessage];
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if (gameObject.GetComponent<Transform>().localPosition.y <= stop)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        if (Input.GetButton(NEXT) == true && activated && currMessage < (messages.Length - 1) && timer > .5)
        {
            currMessage++;
            transform.Find("Text").GetComponent<TextMesh>().text = messages[currMessage];
            timer = 0;
        }

        if (currMessage == (messages.Length - 1) && f)
        {
            Destroy(transform.Find("F").gameObject);
            f = false;
        }
    }

    /**
     * Called when the player walks through the ParrotTrigger
     */
    public void activate()
    {
        if (rb != null)
        {
            //gameObject.GetComponent<MeshRenderer>().enabled = true;
            rb.useGravity = true;
            //rb.AddForce(-Vector3.up * 5);

            activated = true;
            foreach (Transform child in transform)
            {
                if (child.GetComponent<MeshRenderer>() != null)
                {
                    child.GetComponent<MeshRenderer>().enabled = true;
                }
            }
            timer = 0;
        }
    }
}
