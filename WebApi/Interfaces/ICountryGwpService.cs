namespace WebApi.Interfaces
{
    public interface ICountryGwpService
    {
        Task<Dictionary<string, double>> GetForLinesOfBusiness(string countryCode, List<string> linesOfBusinesss, List<int> years = null);
    }
}
