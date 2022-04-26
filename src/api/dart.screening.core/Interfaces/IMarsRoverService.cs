using dart.screening.common.models;

namespace dart.screening.core.Interfaces
{
    public interface IMarsRoverService
    {
        Task<RoverPhotos?> GetPhotosByDate(string earthDate);
    }
}
