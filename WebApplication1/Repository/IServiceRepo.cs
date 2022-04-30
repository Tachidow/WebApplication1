using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IServicesRepo
    {
        IEnumerable<FormModel> FindAlltheList();
    }
}
