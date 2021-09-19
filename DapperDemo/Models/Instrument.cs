using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace DapperDemo.Models
{
    [Table("Instrument")]
    public class Instrument
    {
        [Key]
        public int InstrumentId { get; set; }

        public string Name { get; set; }
    }
}
