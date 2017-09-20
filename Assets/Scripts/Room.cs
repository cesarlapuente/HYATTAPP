/// <summary>
/// A Room, the last element of a NestedListUI
/// </summary>
public class Room : RoomElement
{
    public string[] _imagePaths;

    public Room(string name, string[] imagePaths) : base(name)
    {
        _imagePaths = imagePaths;
    }
}