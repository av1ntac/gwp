using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface ICountryGwpRepository
    {
        Task<List<CountryLineOfBusinessGwp>> List(string countryCode, List<string> linesOfBusiness, List<int> years);
    }
}
