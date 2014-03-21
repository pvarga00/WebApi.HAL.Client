using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Hal.Client.Models;

namespace WebApi.Hal.Client
{
    public class Beers : BaseBeer
    {
        public int TotalResults { get; set; }

        public int TotalPages { get; set; }

        public int Page { get; set; }
        

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("TotalResults: '{0}'\n", TotalResults);
            builder.AppendFormat("TotalPages: '{0}'\n", TotalPages);
            builder.AppendFormat("Page: '{0}'\n", Page);
            foreach (var beer in ResourceList)
            {
                builder.AppendFormat("-Beer: '{0}'\n", beer);
            }
            
            //No To String Yet
            //foreach (var l in _links)
            //{
            //    builder.AppendFormat("--Link: '{0}'\n", _links);
            //}

            return builder.ToString();
        }
    }
}
