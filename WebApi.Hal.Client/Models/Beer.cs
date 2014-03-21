using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Hal.Client.Models;

namespace WebApi.Hal.Client
{
    public class Beer : BaseBeer
    {
        public int? BreweryId { get; set; }
        public string BreweryName { get; set; }

        public int? StyleId { get; set; }
        public string StyleName { get; set; }

        public override string ToString()
        {
            return string.Format("Id: '{0}', Name: '{1}', BreweryName: '{2}', StyleName: '{3}'", Id, Name, BreweryName, StyleName);
        }
    }
}
