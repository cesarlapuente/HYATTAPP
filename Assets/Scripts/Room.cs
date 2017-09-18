public class Room : RoomElement
{
    public string _description;
    public string _imagePath;

    public Room(string name, string description) : base(name)
    {
        _description = description;
    }
}