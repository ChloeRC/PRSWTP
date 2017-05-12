using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CloneLocation {

    private Vector3 location;
    private Vector3 rotation;
    private bool didSword;
    private bool didShoot;
    
    //Constructor
    public CloneLocation(Vector3 loc, Vector3 rot, bool sword, bool shoot)
    {
        location = loc;
        rotation = rot;
        didSword = sword;
        didShoot = shoot;
    }

    public Vector3 getLocation()
    {
        return location;
    }

    public Vector3 getRotation()
    {
        return rotation;
    }

    public bool getDidSword()
    {
        return didSword;
    }

    public bool getDidShoot()
    {
        return didShoot;
    }

    public override string ToString()
    {
        return location.ToString() + " " + rotation.ToString();
    }
}
