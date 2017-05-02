using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunReloadDisplay : FillBar {
    public GameObject player;

	// Use this for initialization
	new void Start () {
		base.full = getReloadTime();
		base.curr = base.full;
		base.originalText = "Gun Reload: ";
		base.Start();
	}

	public int getReloadTime()
    {
		return (4 - player.GetComponent<BulletScript> ().getTime ());
    }

	public new void UpdateText()
	{
		base.curr = getReloadTime();
		base.UpdateText();
	}
}
