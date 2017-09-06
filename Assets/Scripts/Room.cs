using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
    public string _name;
    public string _description;
    public string _imagePath;

    public Room(string name, string description)
    {
        _name = name;
        _description = description;
    }
}
