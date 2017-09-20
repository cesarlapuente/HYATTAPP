/// <summary>
/// A RoomCategory that is used in NestedListUI, can either store a list of RoomCategories or Rooms.
/// </summary>
public class RoomCategory : RoomElement
{
    public RoomElement[] _elements;

    public RoomCategory(string name, RoomElement[] elements) : base(name)
    {
        _elements = elements;
    }
}