namespace dart.screening.common.models
{
    public class RoverPhotos
    {
        public List<Photo>? Photos { get; set; }
    }

    public class Photo
    {
        public int Id { get; set; }
        public int Sol { get; set; }
        public CameraDetails? Camera { get; set; }
        public string Img_Src { get; set; } = string.Empty;
    }

    public class CameraDetails
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Rover_Id { get; set; }
        public string Full_Name { get; set; } = string.Empty;
    }
}