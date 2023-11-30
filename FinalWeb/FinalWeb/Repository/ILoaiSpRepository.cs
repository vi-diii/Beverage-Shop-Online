using FinalWeb.Models;
namespace FinalWeb.Repository
{
    public interface ILoaiSpRepository
    {
        Category Add(Category type );
        Category Update(Category type);
        Category Delete(int typeID);
         Category GetTypeID(int typeID);
         IEnumerable<Category> GetAllType();
    }
}
