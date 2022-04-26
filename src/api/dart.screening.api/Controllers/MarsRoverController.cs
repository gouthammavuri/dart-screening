using dart.screening.common;
using dart.screening.common.models;
using dart.screening.core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;

namespace dart.screening.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarsRoverController : ControllerBase
    {
        public readonly IMarsRoverService _marsRoverService;
        public MarsRoverController(IMarsRoverService marsRoverService)
        {
            _marsRoverService = marsRoverService;
        }

        [HttpGet]
        [Route("photos")]
        public async Task<ActionResult> GetPhotos()
        {
            try
            {
                List<Tuple<string, bool, Guid, RoverPhotos?>> result = new List<Tuple<string, bool, Guid, RoverPhotos?>>();
                string[] inputDates = System.IO.File.ReadAllLines(@"Input.txt");
                foreach (string inputDate in inputDates)
                {
                    var validDate = DateHelper.IsValidDate(inputDate);
                    if (validDate.Item1)
                    {
                        string earthDate = validDate.Item2;
                        if (string.IsNullOrWhiteSpace(earthDate)
                            || !Regex.IsMatch(earthDate, @"^\d{4}\-(0?[1-9]|1[012])\-(0?[1-9]|[12][0-9]|3[01])$"))
                        {
                            result.Add(new Tuple<string, bool, Guid, RoverPhotos?>(inputDate, false, Guid.NewGuid(), null));
                            continue;
                        }

                        RoverPhotos? roverPhotos = await _marsRoverService.GetPhotosByDate(earthDate);
                        result.Add(new Tuple<string, bool, Guid, RoverPhotos?>(inputDate, true, Guid.NewGuid(), roverPhotos));
                    }
                    else
                    {
                        result.Add(new Tuple<string, bool, Guid, RoverPhotos?>(inputDate, false, Guid.NewGuid(), null));
                    }
                }
                return new ObjectResult(result)
                {
                    StatusCode = (int)HttpStatusCode.OK,
                };
            }
            catch(HttpRequestException)
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int)HttpStatusCode.BadGateway,
                };
            }
            catch(Exception)
            {
                return new ObjectResult(null)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }
        }
    }
}
