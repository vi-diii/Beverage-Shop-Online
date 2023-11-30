using FinalWeb.Models;
using FinalWeb.Repository;
using Microsoft.AspNetCore.Mvc;
namespace FinalWeb.ViewComponents
{
    public class LoaiSpMenuViewComponent:ViewComponent
    {
        private readonly ILoaiSpRepository _Type;
        public LoaiSpMenuViewComponent(ILoaiSpRepository TypeRespository)
        {
            _Type =  TypeRespository;
        }
        public IViewComponentResult Invoke()
        {
            var type = _Type.GetAllType().OrderBy(x => x.CategoryName);
            return View(type);
        }
    }
}
