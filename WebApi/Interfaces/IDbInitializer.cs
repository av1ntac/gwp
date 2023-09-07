using WebApi.Data;

namespace WebApi.Interfaces
{
    public interface IDbInitializer
    {
        public void Initialize(GwpDbContext context);
    }
}
