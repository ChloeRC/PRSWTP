﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CloneLocation {

    private Vector3 location;
    private Vector3 rotation;

    //Constructor
    public CloneLocation(Vector3 loc, Vector3 rot)
    {
        location = loc;
        rotation = rot;
    }

    public Vector3 getLocation()
    {
        return location;
    }

    public Vector3 getRotation()
    {
        return rotation;
    }

    public string toString()
    {
        string toReturn = "";

        if (location != null) { toReturn += location.ToString(); }
        else { toReturn += "null"; }

        toReturn += " ";

        if (rotation != null) { toReturn += rotation.ToString(); }
        else { toReturn += "null"; }

        return toReturn;
    }
}