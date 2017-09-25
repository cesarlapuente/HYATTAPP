/// <summary>
/// A Room, the last element of a NestedListUI
/// </summary>
public class RoomPdf : RoomElement
{
    public string _pdfPath;

    public RoomPdf(string name, string pdfPath) : base(name)
    {
        _pdfPath = pdfPath;
    }
}