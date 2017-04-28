using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSphereScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //defaults to playing music, only if not...
		if (!ValueHolder.music)
        {
            GetComponent<AudioSource>().mute = true;
        }
	}
}
