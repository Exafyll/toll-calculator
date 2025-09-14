
namespace TollFeeCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vehicle vehicle = new Vehicle(Vehicle.VehicleType.Car);

            var passes = new[]
            {
                //2013-01-14 (Mondag) Kontrollera att max 60 SEK per dag tas ut
                new DateTime(2013, 01, 14, 06, 05, 00),
                new DateTime(2013, 01, 14, 06, 15, 00),
                new DateTime(2013, 01, 14, 06, 45, 00),
                new DateTime(2013, 01, 14, 07, 20, 00),
                new DateTime(2013, 01, 14, 07, 55, 00),
                new DateTime(2013, 01, 14, 08, 10, 00),
                new DateTime(2013, 01, 14, 08, 35, 00),
                new DateTime(2013, 01, 14, 09, 50, 00),
                new DateTime(2013, 01, 14, 15, 05, 00),
                new DateTime(2013, 01, 14, 15, 35, 00),
                new DateTime(2013, 01, 14, 16, 10, 00),
                new DateTime(2013, 01, 14, 17, 30, 00),
                new DateTime(2013, 01, 14, 18, 20, 00),

                //2013-01-15 (Tisdag) vanligt dag med 7 passeringar
                new DateTime(2013, 01, 15, 06, 25, 00),
                new DateTime(2013, 01, 15, 06, 55, 00),
                new DateTime(2013, 01, 15, 07, 15, 00),
                new DateTime(2013, 01, 15, 08, 25, 00),
                new DateTime(2013, 01, 15, 08, 45, 00),
                new DateTime(2013, 01, 15, 15, 25, 00),
                new DateTime(2013, 01, 15, 15, 40, 00),

                //2013-01-17 (Torsdag) 2 passeringar på 1 timma
                new DateTime(2013, 01, 17, 15, 05, 00),
                new DateTime(2013, 01, 17, 15, 35, 00),

                //2013-01-19 (Lördag) - helgdag
                new DateTime(2013, 01, 19, 07, 45, 00),
                new DateTime(2013, 01, 19, 15, 45, 00),

                //2013-07-15 (Mondag i Juli) - veckodag i en månad utan avgift
                new DateTime(2013, 07, 15, 08, 15, 00),
                new DateTime(2013, 07, 15, 17, 15, 00),
            };
            Array.Sort(passes);

            var calc = new TollCalculator();
            int grandTotal = 0;

            foreach (var group in passes.GroupBy(p => p.Date).OrderBy(g => g.Key))
            {
                var dayArray = group.ToArray();
                int daily = calc.GetTollFee(vehicle, dayArray);

                Console.WriteLine($"{group.Key:yyyy-MM-dd} => {daily} SEK ({dayArray.Length} passes)");
                grandTotal += daily;
            }

            Console.WriteLine($"GRAND TOTAL: {grandTotal} SEK");
        }
    }
}
