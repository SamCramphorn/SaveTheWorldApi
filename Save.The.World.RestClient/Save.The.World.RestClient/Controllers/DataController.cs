using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Save.The.World.RestClient.Context;

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
        public ActionResult<List<Model.Point>> GetCustomerData()
        {
            try
            {
                //using (var transaction = _worldContext.Database.BeginTransaction())
                //{

                //    _worldContext.Point.Add(new Model.Point()
                //    {
                //        Latitude = 2.2,
                //        Longitude = 1.1
                //    });
                //    _worldContext.SaveChanges();
                //    transaction.Commit();

                return _worldContext.Point.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("GetHelperData")]
        public ActionResult<IEnumerable<string>> GetHelperData()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
