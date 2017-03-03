using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour {

    public bool drawn;
    //True for sword-out
    //False for sword-down

    private Transform ts;

    public GameObject healthDisplayer;

    // Use this for initialization
    void Start () {
        ts = GetComponent<Transform>();

        if (drawn) { swordUp(false); }
        else { swordDown(false); }
    }
	
	// Update is called once per frame
	void Update () {

	}

	public void toggleSword(bool direction)
    {
        drawn = !drawn;

        if (drawn) { swordUp(direction); }
        else { swordDown(direction); }
    }

	public void swordUp (bool direction)
    {
		//sword out facing left
		if (direction) {
			ts.localPosition = new Vector3 (-1, 0.15f, 0);
			Quaternion newRot = Quaternion.Euler (-10, 270, 90);
			ts.localRotation = new Quaternion (newRot.x, newRot.y, newRot.z, newRot.w);
			ts.localScale = new Vector3 (0.055f, 0.1f, 0.1f);
		} else {
			//sword out facing right
			ts.localPosition = new Vector3 (1, 0.15f, 0);
			Quaternion newRot = Quaternion.Euler (-10, 90, 90);
			ts.localRotation = new Quaternion (newRot.x, newRot.y, newRot.z, newRot.w);
			ts.localScale = new Vector3 (0.055f, 0.1f, 0.1f);
		}
    }

	public void swordDown(bool direction)
    {
		if (direction) {
			ts.localPosition = new Vector3 (0, 0.15f, 0.55f);
			Quaternion newRot = Quaternion.Euler (78, 90, -90);
			ts.localRotation = new Quaternion (newRot.x, newRot.y, newRot.z, newRot.w);
            ts.localScale = new Vector3 (0.09f, 0.1f, 0.08f);
		} else {
			ts.localPosition = new Vector3 (0, 0.15f, -0.55f);
			Quaternion newRot = Quaternion.Euler (78, 90, -90);
			ts.localRotation = new Quaternion (newRot.x, newRot.y, newRot.z, newRot.w);
            ts.localScale = new Vector3(0.04f, 0.1f, 0.08f);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy" && drawn)
        {
            Debug.Log("Stab");
            col.gameObject.GetComponent<EnemyAI>().kill();
        } else if (col.gameObject.tag == "Enemy" && !drawn) {
            //so the enemy still inflicts damage if your sword isn't drawn
			//also the only method where health is being subtracted
            gameObject.GetComponentInParent<PlayerScript>().health--;
            healthDisplayer.GetComponent<HealthBarDisplay>().UpdateHealth();
        }
    }
}
