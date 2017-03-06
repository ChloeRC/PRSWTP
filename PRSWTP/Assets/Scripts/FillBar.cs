using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillBar : MonoBehaviour {

    public int curr;
    int full;
    float originalWidth;

    public GameObject text;

    public string originalText;

    // Use this for initialization
    void Start()
    {
        full = UpdateValue();
        curr = full;

        UpdateText();
    }

    public int UpdateValue()
    {
        return 0;
    }

    public void UpdateText()
    {
        full = UpdateValue();
        text.GetComponent<TextMesh>().text = originalText + curr + " / " + full;
    }
}
