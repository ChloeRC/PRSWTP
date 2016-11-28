using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    private Rigidbody rb;
    private float gameTicks;
    public float horizSpeed;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        gameTicks = 0.0f;
        rb.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
        gameTicks += Time.deltaTime;
        if (true)
        {
            transform.Translate(Vector2.right * horizSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * horizSpeed * Time.deltaTime);
        }
	}
}
