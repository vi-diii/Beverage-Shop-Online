using FinalWeb.Models;

namespace FinalWeb.Repository
{
    public class LoaiSpRespository : ILoaiSpRepository
    {
        private readonly BeverageRetailContext _context;
        public LoaiSpRespository(BeverageRetailContext context)
        {
            _context = context;
        }

        public Category Add(Category type)
        {
            _context.Categories.Add(type);
            _context.SaveChanges();
            return type;
        }

        public Category Delete(int typeID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAllType()
        {
             return _context.Categories;

        }

        public Category GetTypeID(int typeID)
        {
            return _context.Categories.Find(typeID);
        }

        public Category Update(Category type)
        {
            _context.Update(type);
            _context.SaveChanges();
            return type;
        }
    }
}
