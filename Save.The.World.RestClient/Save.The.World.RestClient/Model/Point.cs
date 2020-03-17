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
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PointId { get; set; }
        [Column("Longitude")]
        public double Longitude { get; set; }
        [Column("Latitude")]
        public double Latitude { get; set; }
    }
}
