using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadMusicSettingScript : MonoBehaviour {

	public bool toggleTo;
	private float gameTicks;
	private static readonly string MUSIC = "music";
		
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
		//if the music is set to this button, turn this button green
		if (toggleTo == ValueHolder.music)
		{
			//GetComponent<TextMesh>().color = Color.white;
            GetComponent<TextMesh>().color = Color.green;
        }
		else //else turn it white
		{
			GetComponent<TextMesh>().color = Color.white;
		}
	}
}
