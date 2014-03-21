using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Hal.Client.Models
{
    public class Review : BaseBeer
    {
        public int Beer_Id { get; set; }
        private string title = "";
        public string Title { get { return title; } set { title = value; Name = value; } }
        public string Content { get; set; }

    }
}
