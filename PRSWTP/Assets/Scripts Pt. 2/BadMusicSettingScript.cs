using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadMusicSettingScript : MonoBehaviour {

	public bool toggleTo;
	private float gameTicks;
	private static readonly string MUSIC = "music";
		
	// Use this for initialization
	void Start () {
		gameTicks = 0.0f;
		
	}
	
	// Update is called once per frame
	void Update () {
		gameTicks += Time.deltaTime;

		//press the m button
		if (Input.GetButton (MUSIC) == true && gameTicks > 0.3f) 
		{
			//toggleTo = !toggleTo;
			gameTicks = 0.0f;
			ValueHolder.music = !ValueHolder.music;
			Debug.Log ("toggled!!");
            Debug.Log("toggleTo: " + toggleTo + " ValueHolder: " + ValueHolder.music);

		}

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
