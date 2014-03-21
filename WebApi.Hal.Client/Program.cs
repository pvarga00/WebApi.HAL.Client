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
            var baseURL = "http://localhost:51665/";
            BaseBeer beers = ParseStuff<Beers>(baseURL, "Beers");           
            Console.WriteLine("Beers: '{0}'\n\n", beers);

            while (true)
            {
                var links = DisplayStuff(beers);
                var selected = Console.ReadLine();
                int selectedIndex = 0;

                Console.Clear();
                if(int.TryParse(selected, out selectedIndex) && selectedIndex > -1 && selectedIndex < links.Count)
                {
                    if (links[selectedIndex].Href.ToLower().Contains("beers"))
                    {
                        beers = ParseStuff<Beers>(baseURL, links[selectedIndex].Href.Trim('~'));
                        Console.WriteLine("Beers: '{0}'\n\n", beers);
                    }
                    else if (links[selectedIndex].Href.ToLower().Contains("beer"))
                    {
                        beers = ParseStuff<Beer>(baseURL, links[selectedIndex].Href.Trim('~'));
                        Console.WriteLine("Beer: '{0}'\n\n", beers);
                    }
                    else if (links[selectedIndex].Href.ToLower().Contains("review"))
                    {
                        beers = ParseStuff<Review>(baseURL, links[selectedIndex].Href.Trim('~'));
                        Console.WriteLine("Review: '{0}'\n\n", beers);
                    }
                    else
                    {
                        beers = ParseStuff<BaseBeer>(baseURL, links[selectedIndex].Href.Trim('~'));
                        Console.WriteLine("Data: '{0}'\n\n", beers);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Number, Please Try Again.");
                    Console.WriteLine("Beers: '{0}'\n\n", beers);
                }
            }

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

            if (thingToDisplay.ResourceList != null)
            {
                foreach (var thing in thingToDisplay.ResourceList)
                {
                    foreach (var links in thing._links.Where(p => p.IsTemplated == false))
                    {
                        Console.WriteLine("Option {0}: Action: '{2}' Name: '{1}' Link: '{3}'", index, thing.Name, links.Rel, links.Href);
                        result.Add(links);
                        index++;
                    }
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
