using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Save.The.World.RestClient.Model
{
    public class CollectivePoints
    {
        [Key]
        public int CollectivePointsId { get; set; }
        public Point PointId { get; set; }
        public Driver DriverId { get; set; }

        //public virtual ICollection<Point> Points { get; set; }
        //public virtual Driver Driver { get; set; }

    }
}
