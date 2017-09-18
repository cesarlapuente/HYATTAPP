/// <summary>
/// Simple image container, mainly used in Carousel.cs
/// </summary>
public class ImageContainer
{
    public string _path;
    public string _name;
    public string _copyright;

    public ImageContainer(string path, string name, string copyright)
    {
        _path = path;
        _name = name;
        _copyright = copyright;
    }

    public ImageContainer(string path)
    {
        _path = path;
        _name = "";
        _copyright = "";
    }
}