using dart.screening.common.models;
using dart.screening.core;
using dart.screening.core.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace dart.screening.tests
{
    public class MarsRoverServiceTests
    {
        private IMarsRoverService? _marsRoverService;
        private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
        private readonly Mock<ILogger<MarsRoverService>> _mockLogger;
        private readonly Mock<IOptions<MarsRoverOptions>> _mockOptions;

        public MarsRoverServiceTests()
        {
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockLogger = new Mock<ILogger<MarsRoverService>>();
            _mockOptions = new Mock<IOptions<MarsRoverOptions>>();
        }

        [SetUp]
        public void Setup()
        {
            _mockOptions.SetupGet(x => x.Value).Returns(new MarsRoverOptions()
            {
                Api_Key = "977VVWahutNF6FK3FGhXIm4dG7WbGqt7p2Zfuy5n",
                Api_Url = "https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos"
            });
        }

        [Test]
        public async Task GetPhotosByDate_ReturnsRoverPhotos()
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(new RoverPhotos()
                    {
                        Photos = new List<Photo>()
                        {
                            new Photo()
                            {
                                Id = 1,
                                Sol = 2,
                                Img_Src = "http://mars.jpl.nasa.gov/msl-raw-images/proj/msl/redops/ods/surface/sol/01622/opgs/edr/fcam/FLB_541484941EDR_F0611140FHAZ00341M_.JPG",
                                Camera = new CameraDetails()
                                {
                                    Id = 1,
                                    Full_Name = "Front Hazard Avoidance Camera",
                                    Name = "FHAZ",
                                    Rover_Id = 2                                }
                            }
                        }
                    }))
                });
            HttpClient httpClient = new HttpClient(mockMessageHandler.Object);
            _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _marsRoverService = new MarsRoverService(_mockHttpClientFactory.Object, _mockOptions.Object, _mockLogger.Object);
            RoverPhotos? roverPhotos = await _marsRoverService.GetPhotosByDate("2021-10-10");
            Assert.IsNotNull(roverPhotos);
            Assert.IsTrue(roverPhotos?.Photos?.Count == 1);
            Assert.Pass();
        }

        [Test]
        public void GetPhotosByDate_ThrowsHttpRequestException()
        {
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError
                });
            HttpClient httpClient = new HttpClient(mockMessageHandler.Object);
            _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);

            _marsRoverService = new MarsRoverService(_mockHttpClientFactory.Object, _mockOptions.Object, _mockLogger.Object);
            Task roverPhotos() => _marsRoverService.GetPhotosByDate("2021-10-10");
            Assert.ThrowsAsync<HttpRequestException>(roverPhotos);
        }
    }
}