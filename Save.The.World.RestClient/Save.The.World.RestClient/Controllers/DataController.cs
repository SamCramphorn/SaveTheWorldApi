using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Save.The.World.RestClient.Context;
using Save.The.World.RestClient.Model;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;

namespace Save.The.World.RestClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly WorldContext _worldContext;
        public DataController(WorldContext worldContext) 
        {
            _worldContext = worldContext;
        }

        [HttpGet("GetCustomerData")]
        public ActionResult GetCustomerData()
        {
            try
            {
                PopulateData();
                return Ok(_worldContext.User.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return BadRequest(ex);
            }
        }

        private void PopulateData()
        {
            string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
            string ApplicationName = "Google Sheets API .NET Quickstart";

            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var spreadsheetId = "1c-Ub4EgyOyxlmMoaweKub-1lbnOlSecX-caX7LUeHGo";
            var range = "Sheet1!A2:E";
            var request = service.Spreadsheets.Values.Get(spreadsheetId, range);
            var response = request.Execute();

            IList<IList<object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    try
                    {
                        CreateUserModel(row[0], row[3]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            else
                Console.WriteLine("No data found.");
        }

        private bool CreateUserModel(object userId, object coordinateContents)
        {
            //using (var transaction = _worldContext.Database.BeginTransaction())
            //{
            if (int.TryParse(userId.ToString(), out var id) && !_worldContext.User.Any(x => x.UserId == id))
            {
                var user = new User()
                {
                    UserId = id,
                    Points = new List<Point>()
                };

                var contents = coordinateContents.ToString();
                if (string.IsNullOrEmpty(contents) || contents.Equals("null", StringComparison.InvariantCultureIgnoreCase))
                    return false;


                var content = contents.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                var points = new List<Point>();

                foreach (var coordinates in content)
                {
                    var coordinate = coordinates.Split(',');
                    if (coordinate.Count() == 2)
                    {
                        points.Add(new Point()
                        {
                            PointId = Guid.NewGuid(),
                            Latitude = double.Parse(coordinate[0]),
                            Longitude = double.Parse(coordinate[1]),
                            User = user,
                            UserId = user.UserId
                        });
                    }
                    if (points.Any())
                    {
                        _worldContext.User.Add(user);
                        points.ForEach(x => _worldContext.Point.Add(x));
                    }
                }
            }

            _worldContext.SaveChanges();
            //transaction.Commit();
            return true;
            //}
        }
    }
}
