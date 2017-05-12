using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class swordsound : MonoBehaviour {
    public SwordScript other;
    public void Kaching()
    {
        bool swordUp; 

        other.GetComponent<AudioSource>().Play();
    }
}