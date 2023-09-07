using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class CountryGwpRepository: ICountryGwpRepository
    {
        private readonly GwpDbContext _gwpDbContext;

        public CountryGwpRepository(GwpDbContext gwpDbContext)
        {
            _gwpDbContext = gwpDbContext;
        }

        public async Task<List<CountryLineOfBusinessGwp>> List(string countryCode, List<string> linesOfBusiness, List<int> years)
        {
            var result = await _gwpDbContext.CountryLineOfBusinessGwt.Include(x => x.LineOfBusiness)
                .Where(x => x.Country.Code == countryCode & linesOfBusiness.Contains(x.LineOfBusiness.Name) && years.Contains(x.Year))
                .ToListAsync();
            
            return result;
        }
    }
}
