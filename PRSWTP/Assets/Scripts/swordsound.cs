using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class swordsound : MonoBehaviour {
    public SwordScript other;
    public void Kaching()
    {
        other.GetComponent<AudioSource>().Play();
    }
}