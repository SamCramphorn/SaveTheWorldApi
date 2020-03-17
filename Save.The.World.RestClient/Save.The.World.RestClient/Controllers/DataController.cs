using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Save.The.World.RestClient.Context;
using Save.The.World.RestClient.Model;
using Google.Apis.Auth.OAuth2;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
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

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1c-Ub4EgyOyxlmMoaweKub-1lbnOlSecX-caX7LUeHGo";
            String range = "Sheet1!A2:E";
            SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                Console.WriteLine("Name, Major");
                foreach (var row in values)
                {
                    try
                    {
                        CreateUserModel(row[0], row[3]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.InnerException);
                    }
                }
            }
            else
                Console.WriteLine("No data found.");
        }

        private bool CreateUserModel(object userId, object coordinateContents)
        {
            using (var transaction = _worldContext.Database.BeginTransaction())
            {
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
                                User = user
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
                transaction.Commit();
                return true;
            }
        }
    }
}
