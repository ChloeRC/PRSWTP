using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKillScript : MonoBehaviour {

    public GameObject enemy;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            enemy.GetComponent<BossAI>().kill();
        }
    }
}
