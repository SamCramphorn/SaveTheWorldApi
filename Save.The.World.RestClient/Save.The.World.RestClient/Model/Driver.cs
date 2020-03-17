using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Save.The.World.RestClient.Model
{
    public class Driver
    {
        [Key]
        public int DriverId { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public bool NeedsHelp { get; set; }

        public ICollection<Driver> Drivers { get; set; }

    }
}
