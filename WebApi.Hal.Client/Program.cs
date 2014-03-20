using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Hal.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var beers = ParseStuff<Beers>("http://localhost:51665/", "Beers");           
            Console.WriteLine("Beers: '{0}'\n\n", beers.ToString());

            while (beers.Page < beers.TotalPages)
            {
                beers = ParseStuff<Beers>("http://localhost:51665/", beers._links
                                                                .Single(p => p.Rel == "next")
                                                                .Href
                                                                .Trim('~'));

                Console.WriteLine("Beers: '{0}'\n\n", beers);
            }

            var beer = ParseStuff<Beer>("http://localhost:51665/", beers.ResourceList[0]._links[0].Href.Trim('~'));           
            Console.WriteLine("Let's try the first: '{0}'", beer);
            Console.ReadKey();
        }

        public static T ParseStuff<T>(string baseUri, string link) where T : class
        {
            T result = default(T);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                var beersTask = client.GetStringAsync(link);
                beersTask.Wait();
                var beersToDisplay = beersTask.Result;
                result = JsonConvert.DeserializeObject<T>(beersToDisplay);
            }

            return result;
        }
    }
}
