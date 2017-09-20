/// <summary>
/// A RoomElement class can be used in a NestedListUI
/// Can either be a RoomCategory or a Room
/// </summary>
public abstract class RoomElement
{
    public string _name;

    public RoomElement(string name)
    {
        _name = name;
    }
}