using WebApi.Interfaces;

namespace WebApi.Services
{
    public class CountryGwpService: ICountryGwpService
    {
        private readonly List<int> defaultYears = new() { 2008, 2009, 2010, 2011, 2012, 2013, 2014, 2015 };

        private readonly ICountryGwpRepository _countryGwtRepository;

        public CountryGwpService(ICountryGwpRepository countryGwtRepository)
        {
            _countryGwtRepository = countryGwtRepository;
        }

        public async Task<Dictionary<string, double>> GetForLinesOfBusiness(string countryCode, List<string> linesOfBusinesss, List<int> years = null)
        {
            years ??= defaultYears;

            var list = await _countryGwtRepository.List(countryCode, linesOfBusinesss, years);

            var lookup = list.ToLookup(x => x.LineOfBusiness.Name);
            var result = lookup.ToDictionary(x => x.Key, x => x.Average(y => y.Gwp));

            return result;
        }
    }
} 
