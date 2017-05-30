using System;
using System.Linq;
using LinqToTerraServerProvider;

[assembly: CLSCompliant(true)]
namespace ClientApp
{
    public sealed class Program
    {
        static void Main()
        {
            QueryableTerraServerData<Place> terraPlaces = new QueryableTerraServerData<Place>();

            //QUERY 1
            //var query = from place in terraPlaces
            //            where place.Name == "London"
            //            select place.PlaceType;

            //QUERY 2
            //var query = from place in terraPlaces
            //            where place.Name.StartsWith("Lond")
            //            select new { place.Name, place.State };


            //QUERY 3
            string[] places = { "Johannesburg", "Yachats", "Seattle" };

            var query = from place in terraPlaces
                        where places.Contains(place.Name)
                        orderby place.State
                        select new { place.Name, place.State };

            foreach (var obj in query)
                Console.WriteLine(obj);
        }

        private Program() { }
    }
}
