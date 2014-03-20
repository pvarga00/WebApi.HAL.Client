using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Hal.Client
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? BreweryId { get; set; }
        public string BreweryName { get; set; }

        public int? StyleId { get; set; }
        public string StyleName { get; set; }

        public Link[] _links { get; set; }

        public override string ToString()
        {
            return string.Format("Id: '{0}', Name: '{1}'", Id, Name);
        }
    }
}
