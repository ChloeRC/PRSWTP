using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class swordsound : MonoBehaviour {
    public SwordScript other;
    void Kaching()
    {
        other.GetComponent<AudioSource>().Play();
    }
}