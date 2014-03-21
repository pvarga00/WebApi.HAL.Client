using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Hal.Client.Models
{
    public class BaseBeer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<BaseBeer> ResourceList { get; set; }

        public Link[] _links { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0} Name: {1}", Id, Name);
        }
    }
}
