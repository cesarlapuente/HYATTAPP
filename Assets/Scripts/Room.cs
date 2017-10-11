using System.IO;
using UnityEngine;

/// <summary>
/// A Room, the last element of a NestedListUI
/// </summary>
public class Room : RoomElement
{
    public int _id;
    public string[] _imagePaths;
    public string _imageFolderPath;

    public Room(int id, string name, string[] imagePaths) : base(name)
    {
        _imagePaths = imagePaths;
        _id = id;
    }

    public Room (int id, string name, string imageFolderPath) : base(name)
    {
        _imageFolderPath = imageFolderPath;
        _id = id;
    }
}