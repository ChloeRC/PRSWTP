using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillBar : MonoBehaviour {

    public float curr;
    public float full;
	public float currWidth;
	public float currPos;
    public float originalWidth;

    public bool useDivide;

    public GameObject text;

    public string originalText;

    // Use this for initialization
    public void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (useDivide) { text.GetComponent<TextMesh>().text = originalText + curr + " / " + full; }
        else { text.GetComponent<TextMesh>().text = originalText; }
        
        float currWidth = originalWidth * ((float)curr / (float)full);
        if (!double.IsNaN(currWidth)) { gameObject.transform.localScale = new Vector3(currWidth, 0.27f, 0.1f); }
        else { gameObject.transform.localScale = new Vector3(0, 0.27f, 0.1f); }

        float currPos = -(originalWidth / 2f) + (.5f) * currWidth;
        if (!double.IsNaN(currPos)) { gameObject.transform.localPosition = new Vector3(currPos, 0, -0.1f); }
        else { gameObject.transform.localPosition = new Vector3(0, 0, -0.1f); }
    }
}
