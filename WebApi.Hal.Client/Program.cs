using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApi.Hal.Client.Models;

namespace WebApi.Hal.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var beers = ParseStuff<Beers>("http://localhost:51665/", "Beers");           
            Console.WriteLine("Beers: '{0}'\n\n", beers.ToString());

            while (true)
            {
                DisplayStuff(beers);
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

        public static List<Link> DisplayStuff<T>(T thingToDisplay) where T : BaseBeer
        {
            List<Link> result = new List<Link>();
            int index = 0;

            foreach (var links in thingToDisplay._links.Where(p => p.IsTemplated == false))
            {
                Console.WriteLine("Option {0}: Action: '{1}' Link: '{2}'", index, links.Rel, links.Href);
                result.Add(links);
                index++;
            }

            foreach(var thing in thingToDisplay.ResourceList)
            {
                foreach (var links in thing._links.Where(p => p.IsTemplated == false))
                {
                    Console.WriteLine("Option {0}: Action: '{2}' Name: '{1}' Link: '{3}'", index, thing.Name, links.Rel, links.Href);
                    result.Add(links);
                    index++;
                }
            }

            return result;
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
