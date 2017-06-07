using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSettingScript : MonoBehaviour {

    public bool toggleTo;

    void OnMouseDown()
    {
        ValueHolder.music = toggleTo;
        //set music to the bool passed in
    }

    void Update()
    {
        //if the music is set to this button, turn this button green
        if (toggleTo == ValueHolder.music)
        {
            GetComponent<TextMesh>().color = Color.green;
        }
        else //else turn it white
        {
            GetComponent<TextMesh>().color = Color.white;
        }
    }
}
