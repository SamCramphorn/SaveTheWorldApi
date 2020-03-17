using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Save.The.World.RestClient.Model
{
    public class TotalPoints
    {
        [Key]
        [Column("TotalPointsId")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TotalPointsId { get; set; }

        //[ForeignKey("User")]
        //public int UserId { get; set; }
        public virtual Model.User User { get; set; }

        //[ForeignKey("Point")]
        //public int PointId { get; set; }
        public virtual Point Point { get; set; }
    }
}
