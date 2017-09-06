using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCategory {
    public string _name;
    public Room[] _rooms;

    public RoomCategory(string name, Room[] rooms)
    {
        _name = name;
        _rooms = rooms;
    }
}
