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

            var query = from place in terraPlaces
                        where place.Name == "London"
                        select place.PlaceType;

            foreach (PlaceType placeType in query)
                Console.WriteLine(placeType);
        }

        private Program() { }
    }
}
