namespace WebApi.Models
{
    public class CountryLineOfBusinessGwp
    {
        public int Id { get; set; }
        public Country Country { get; set; }    
        public int CountryId { get; set; }
        public LineOfBusiness LineOfBusiness { get; set; }
        public int LineOfBusinessId { get; set; }
        public double Gwp { get; set; }
        public int Year { get; set; }
    }
}
