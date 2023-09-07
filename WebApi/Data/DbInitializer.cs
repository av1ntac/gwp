using CsvHelper; 
using System.Reflection;
using System.Text;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Data
{
    public class DbInitializer : IDbInitializer
    {
        private const string ResourceName = "WebApi.Data.gwpByCountry.csv";
        private const int StartingYear = 2000;
        private const int YearsTotal = 16;

        public void Initialize(GwpDbContext context)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(ResourceName))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture);
                    var lines = csvReader.GetRecords<CsvRow>().ToList();

                    var countries = lines.Select(x => x.country).Distinct().Select(x => new Country { Code = x }).ToList();
                    context.Country.AddRange(countries);

                    var lobs = lines.Select(x => x.lineOfBusiness).Distinct().Select(x => new LineOfBusiness { Name = x }).ToList();
                    context.LineOfBusiness.AddRange(lobs);

                    foreach(var line in lines)
                    {
                        var currentCountry = countries.First(x => x.Code == line.country);
                        var currentLob = lobs.First(x => x.Name == line.lineOfBusiness);

                        foreach(var year in Enumerable.Range(StartingYear, YearsTotal))
                        {
                            AddGwp(line, currentCountry, currentLob, year, context);
                        }
                    }

                    context.SaveChanges();
                }
            }
        }

        private static void AddGwp(CsvRow line, Country currentCountry, LineOfBusiness currentLob, int year, GwpDbContext context)
        {
            var gwp = line.GetType().GetProperty($"Y{year}")?.GetValue(line) as double?;
            
            if (gwp.HasValue)
            {
                var result = new CountryLineOfBusinessGwp
                {
                    Country = currentCountry,
                    LineOfBusiness = currentLob,
                    Year = year,
                    Gwp = gwp.Value
                };
                context.CountryLineOfBusinessGwt.Add(result);
            }
        }
    }
}