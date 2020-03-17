using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Save.The.World.RestClient.Model
{
    public class User
    {
        [Key]
        [Column("UserId")]
        public int UserId { get; set; }
        public IList<Point> Points{ get; set; }

    }
}
