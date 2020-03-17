using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Save.The.World.RestClient.Context;
using Save.The.World.RestClient.Model;

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

                //var users = new List<UserPoints>();

                //foreach (var data in _worldContext.TotalPoints)
                //{
                //    if (users.Any(x => x.User.UserId == data.User.UserId))
                //        users.Find(x => x.User.UserId == data.User.UserId).Points.Add(data.Point);
                //    else
                //    {
                //        var points = data.
                //        var newUser = new UserPoints()
                //        {
                //            User = data.User,
                //            Points = new List<Point>{
                //                data.User.TotalPoints.Select(x => x.Point).ToList();
                //        }
                //        }
                //}

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
            using (var transaction = _worldContext.Database.BeginTransaction())
            {
                var user = new Model.User()
                {
                    Name = "Sam",
                    Address = "1 Abington Grove",
                    NeedsHelp = false,
                    Number = "01604412345",
                };

                var points = new List<Model.Point>()
            {
                new Model.Point(){Latitude =52.250624,Longitude =-0.879991},
                new Model.Point(){Latitude =52.257395,Longitude =-0.872117},
                new Model.Point(){Latitude =52.256029,Longitude =-0.870743},
                new Model.Point(){Latitude =52.255162,Longitude =-0.862976},
                new Model.Point(){Latitude =52.252444,Longitude =-0.861335},
                new Model.Point(){Latitude =52.250637,Longitude =-0.857   },
                new Model.Point(){Latitude =52.246394,Longitude =-0.85597 },
                new Model.Point(){Latitude =52.245632,Longitude =-0.84404 },
                new Model.Point(){Latitude =52.242742,Longitude =-0.845757},
                new Model.Point(){Latitude =52.240193,Longitude =-0.86127 },
                new Model.Point(){Latitude =52.245149,Longitude =-0.864551},
                new Model.Point(){Latitude =52.248562,Longitude =-0.868371},
                new Model.Point(){Latitude =52.245619,Longitude =-0.87028 },
                new Model.Point(){Latitude =52.243506,Longitude =-0.872638},
                new Model.Point(){Latitude =52.241667,Longitude =-0.879247},
                new Model.Point(){Latitude =52.245398,Longitude =-0.883023}
            };

                _worldContext.User.Add(user);
                points.ForEach(x => _worldContext.Point.Add(x));

                var totalPoints = new List<Model.TotalPoints>();

                foreach (var point in points)
                {
                    totalPoints.Add(new Model.TotalPoints()
                    {
                        Point = point,
                        User = user,
                    });
                }
                totalPoints.ForEach(x => _worldContext.TotalPoints.Add(x));

                _worldContext.SaveChanges();
                transaction.Commit();
            }
        }
    }
}
