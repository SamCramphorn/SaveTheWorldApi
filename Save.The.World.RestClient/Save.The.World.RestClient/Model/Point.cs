using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Save.The.World.RestClient.Model
{
    public class Point
    {
        [Key]
        [Column("PointId")]
        [JsonIgnore]
        public Guid PointId { get; set; }
        [Column("Longitude")]
        public double Longitude { get; set; }
        [Column("Latitude")]
        public double Latitude { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual Model.User User { get; set; }
    }
}
