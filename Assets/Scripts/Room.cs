using System.IO;
using UnityEngine;

/// <summary>
/// A Room, the last element of a NestedListUI
/// </summary>
public class Room : RoomElement
{
    public string[] _imagePaths;
    public string _imageFolderPath;

    public Room(string name, string[] imagePaths) : base(name)
    {
        _imagePaths = imagePaths;
    }

    public Room (string name, string imageFolderPath) : base(name)
    {
        _imageFolderPath = imageFolderPath;
    }
}