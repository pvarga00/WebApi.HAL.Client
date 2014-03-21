using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Hal.Client.Models
{
    public class BeerDetail : BaseBeer
    {
        BaseBeer Style { get; set; }

        BaseBeer Brewery { get; set; }

        List<Review> Reviews { get; set; }
    }
}
