public class RoomCategory : RoomElement
{
    public RoomElement[] _elements;

    public RoomCategory(string name, RoomElement[] elements) : base(name)
    {
        _elements = elements;
    }
}